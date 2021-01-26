
using DiShare.Domain.Models;
using DiShare.Logic.DownloadQueue.Enums;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DiShare.Logic.DownloadQueue
{
  public interface IDownloadTaskQueue
  {
    event EventHandler<DownloadQueueEventArgs> DownloadQueueChanged;

    event EventHandler<DownloadQueueStateEventArgs> DownloadQueueStateChanged;

    ObservableCollection<ModelItem> Queue { get; }

    DownloadQueueState State { get; }

    Task AddTaskAsync(ModelItem item);

    ModelItem GetQueuedItem(ModelItem item);

    Task Init();

    void Start();

    void StopAll();

    bool IsNeedToDownload();
  }
}
