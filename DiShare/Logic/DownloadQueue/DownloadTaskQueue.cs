

using DiShare.Api.ItemDownloaderApi;
using DiShare.Archives;
using DiShare.Data.CacheFolderProvider;
using DiShare.Data.Exceptions;
using DiShare.Data.Providers;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using DiShare.Logic.DownloadQueue.Enums;
using DiShare.Logic.ErrorHandler.Models;
using DiShare.Logic.ExceptionHandler;
using DiShare.Logic.HashSumChecker;
using DiShare.Logic.Users;
using DiShare.Logic.Users.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.DownloadQueue
{
  public class DownloadTaskQueue : IDownloadTaskQueue
  {
    private readonly IItemDownloaderApi _itemDownloaderApi;
    private readonly ICacheFolderProvider _cacheFolderProvider;
    private readonly IExceptionHandler _exceptionHandler;
    private readonly IArchiveExtractor _archiveExtractor;
    private readonly IItemsProvider _itemsProvider;
    private readonly IUserProvider _userProvider;
    private readonly IHashSumChecker _hashSumChecker;
    private readonly AsyncLock _asyncLock;

    public ObservableCollection<ModelItem> Queue { get; } = new ObservableCollection<ModelItem>();

    public DownloadQueueState State { get; private set; }

    public event EventHandler<DownloadQueueEventArgs> DownloadQueueChanged;

    public event EventHandler<DownloadQueueStateEventArgs> DownloadQueueStateChanged;

    public DownloadTaskQueue(
      IItemDownloaderApi itemDownloaderApi,
      ICacheFolderProvider cacheFolderProvider,
      IExceptionHandler exceptionHandler,
      IArchiveExtractor archiveExtractor,
      IItemsProvider itemsProvider,
      IUserProvider userProvider,
      IHashSumChecker hashSumChecker)
    {
      this._itemDownloaderApi = itemDownloaderApi;
      this._cacheFolderProvider = cacheFolderProvider;
      this._exceptionHandler = exceptionHandler;
      this._archiveExtractor = archiveExtractor;
      this._itemsProvider = itemsProvider;
      this._userProvider = userProvider;
      this._hashSumChecker = hashSumChecker;
      this._asyncLock = new AsyncLock();
    }

    public async void StopAll()
    {
      if (!this.Queue.Any<ModelItem>((Func<ModelItem, bool>) (i => i.State != ItemState.Cached)))
        return;
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        if (this.Queue.Any<ModelItem>((Func<ModelItem, bool>) (i => i.State != ItemState.Cached)))
        {
          ModelItem[] modelItemArray = this.Queue.Where<ModelItem>((Func<ModelItem, bool>) (i => i.State != ItemState.Cached)).ToArray<ModelItem>();
          for (int index = 0; index < modelItemArray.Length; ++index)
          {
            ModelItem item = modelItemArray[index];
            item.State = ItemState.NotCached;
            item.TokenSource?.Cancel();
            TryResult tryResult1 = await this._itemsProvider.UpdateItemAsync(item).ConfigureAwait(false);
            TryResult tryResult2 = await this._itemsProvider.ClearCacheItem(item).ConfigureAwait(false);
            this.Queue.Remove(item);
            item = (ModelItem) null;
          }
          modelItemArray = (ModelItem[]) null;
        }
      }
    }

    public bool IsNeedToDownload() => this.Queue.Any<ModelItem>((Func<ModelItem, bool>) (i => i.State != ItemState.Cached));

    public async Task Init()
    {
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        TryResult<IReadOnlyCollection<ModelItem>> tryResult1 = await this._itemsProvider.GetItemsAsync().ConfigureAwait(false);
        if (tryResult1.IsFaulted)
        {
          if (tryResult1.Exception is CacheException)
            return;
          this._exceptionHandler.HandleException(tryResult1.Exception);
          return;
        }
        List<ModelItem> downloading = tryResult1.Value.Where<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Downloading || i.State == ItemState.DownloadingError || i.State == ItemState.Extraction)).ToList<ModelItem>();
        List<ModelItem> queued = tryResult1.Value.Where<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Queued)).ToList<ModelItem>();
        foreach (ModelItem modelItem in downloading)
        {
          modelItem.State = ItemState.Queued;
          TryResult tryResult2 = await this._itemsProvider.UpdateItemAsync(modelItem).ConfigureAwait(false);
        }
        downloading.AddRange((IEnumerable<ModelItem>) queued);
        foreach (ModelItem modelItem in downloading)
          this.Queue.Add(modelItem);
        downloading = (List<ModelItem>) null;
        queued = (List<ModelItem>) null;
      }
    }

    public async Task AddTaskAsync(ModelItem item)
    {
      DownloadTaskQueue downloadTaskQueue = this;
      if (item.State == ItemState.Queued && item.State == ItemState.Downloading && item.State == ItemState.Extraction)
        return;
      using (await downloadTaskQueue._asyncLock.LockAsync().ConfigureAwait(false))
      {
        if (item.State == ItemState.Queued && item.State == ItemState.Downloading)
        {
          if (item.State == ItemState.Extraction)
            goto label_9;
        }
        item.State = ItemState.Queued;
        item.Progress = 0;
      }
label_9:
      await downloadTaskQueue.UpdateItem(item);
      // ISSUE: explicit non-virtual call
      (downloadTaskQueue.Queue).Add(item);
      EventHandler<DownloadQueueEventArgs> downloadQueueChanged = downloadTaskQueue.DownloadQueueChanged;
      if (downloadQueueChanged != null)
      {
        // ISSUE: explicit non-virtual call
        // ISSUE: explicit non-virtual call
        downloadQueueChanged((object) downloadTaskQueue, new DownloadQueueEventArgs( (downloadTaskQueue.Queue).Count,  (downloadTaskQueue.Queue).Count<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Cached))));
      }
      // ISSUE: explicit non-virtual call
       downloadTaskQueue.Start();
    }

    public void Start() => Task.Run(new Func<Task>(this.StartAsync));

    private async Task StartAsync()
    {
      DownloadTaskQueue downloadTaskQueue = this;
      // ISSUE: explicit non-virtual call
      if ( (downloadTaskQueue.Queue).All<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Cached)))
      {
        using (await downloadTaskQueue._asyncLock.LockAsync().ConfigureAwait(false))
        {
          // ISSUE: explicit non-virtual call
          if ( downloadTaskQueue.Queue.All<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Cached)))
          {
            // ISSUE: explicit non-virtual call
             (downloadTaskQueue.Queue).Clear();
            downloadTaskQueue.SetState(DownloadQueueState.Finished);
            return;
          }
        }
      }
      downloadTaskQueue.SetState(DownloadQueueState.Downloading);
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      if ( downloadTaskQueue.Queue.Any<ModelItem>() && downloadTaskQueue.Queue.Count<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Downloading)) >= 5)
      {
        using (await downloadTaskQueue._asyncLock.LockAsync().ConfigureAwait(false))
        {
          // ISSUE: explicit non-virtual call
          if (! (downloadTaskQueue.Queue).Any<ModelItem>())
          {
            // ISSUE: explicit non-virtual call
            if ( (downloadTaskQueue.Queue).Count<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Downloading)) >= 5)
              return;
          }
        }
      }
      // ISSUE: explicit non-virtual call
      ModelItem item =  (downloadTaskQueue.Queue).FirstOrDefault<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Queued));
      if (item == null)
        return;
      try
      {
        if (item.TokenSource == null)
          item.TokenSource = new CancellationTokenSource();
        item.State = ItemState.Downloading;
        await downloadTaskQueue.UpdateItem(item);
        TryResult<string> result = await downloadTaskQueue._cacheFolderProvider.GetFolderAsync().ConfigureAwait(false);
        if (result.IsFaulted)
        {
          item.State = ItemState.DownloadingError;
          return;
        }
        TryResult<UserInfo> tryResult1 = await downloadTaskQueue._userProvider.GetUserInfoAsync().ConfigureAwait(false);
        string token1 = tryResult1.IsFaulted ? string.Empty : tryResult1.Value.Token;
        string path = Path.Combine(result.Value, "Cache", item.Id, "item.7z");
        TryResult res = await downloadTaskQueue._itemDownloaderApi.TryDownloadItemAsync(item.Id, path, token1, (Action<ProgressChangedEventArgs>) (progress => item.Progress = progress.ProgressPercentage), item.TokenSource.Token);
        CancellationToken token2 = item.TokenSource.Token;
        token2.ThrowIfCancellationRequested();
        if (res.IsFaulted)
        {
          downloadTaskQueue._exceptionHandler.HandleException(res.Exception, notificationMode: (res.Exception is WebException || res.Exception is HttpRequestException ? NotificationMode.Suppress : NotificationMode.Notify));
          item.State = ItemState.DownloadingError;
          await downloadTaskQueue.UpdateItem(item);
        }
        else
        {
          TryResult<bool> tryResult2 = downloadTaskQueue._hashSumChecker.CheckFile(path, item.Hash);
          if (tryResult2.IsFaulted || !tryResult2.Value)
          {
            item.State = ItemState.DownloadingError;
            item.Progress = 0;
            await downloadTaskQueue.UpdateItem(item);
          }
          else
          {
            item.State = ItemState.Extraction;
            item.Progress = 0;
            TryResult<bool> async = await downloadTaskQueue._archiveExtractor.ExtractAsync(path, Path.GetDirectoryName(path), cancellationToken: item.TokenSource.Token);
            path.RemoveFileSafe();
            token2 = item.TokenSource.Token;
            token2.ThrowIfCancellationRequested();
            if (async.IsFaulted)
            {
              downloadTaskQueue._exceptionHandler.HandleException(res.Exception);
              item.State = ItemState.DownloadingError;
              await downloadTaskQueue.UpdateItem(item);
            }
            else
            {
              item.State = ItemState.Cached;
              await downloadTaskQueue.UpdateItem(item);
            }
          }
        }
        EventHandler<DownloadQueueEventArgs> downloadQueueChanged = downloadTaskQueue.DownloadQueueChanged;
        if (downloadQueueChanged != null)
        {
          // ISSUE: explicit non-virtual call
          // ISSUE: explicit non-virtual call
          downloadQueueChanged((object) downloadTaskQueue, new DownloadQueueEventArgs( downloadTaskQueue.Queue.Count, downloadTaskQueue.Queue.Count<ModelItem>((Func<ModelItem, bool>) (i => i.State == ItemState.Cached))));
        }
        result = new TryResult<string>();
        path = (string) null;
        res = new TryResult();
      }
      catch (TaskCanceledException ex)
      {
        item.State = ItemState.DownloadingError;
      }
      finally
      {
        item.TokenSource = (CancellationTokenSource) null;
      }
      // ISSUE: explicit non-virtual call
      downloadTaskQueue.Start();
    }

    private async Task UpdateItem(ModelItem item)
    {
      TryResult tryResult = await this._itemsProvider.UpdateItemAsync(item).ConfigureAwait(false);
      if (!tryResult.IsFaulted)
        return;
      this._exceptionHandler.HandleException(tryResult.Exception, notificationMode: NotificationMode.Suppress);
    }

    public ModelItem GetQueuedItem(ModelItem item) => this.Queue.FirstOrDefault<ModelItem>((Func<ModelItem, bool>) (i => i.Id.Equals(item.Id))) ?? item;

    private void SetState(DownloadQueueState state)
    {
      this.State = state;
      EventHandler<DownloadQueueStateEventArgs> queueStateChanged = this.DownloadQueueStateChanged;
      if (queueStateChanged == null)
        return;
      queueStateChanged((object) this, new DownloadQueueStateEventArgs(state));
    }
  }
}
