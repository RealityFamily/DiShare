

using DiShare.Infrastructure;
using DiShare.Infrastructure.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Logic.Network
{
  public class SimpleFileDownloader : ISimpleFileDownloader
  {
    public async Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      IReadOnlyDictionary<string, string> headers = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      try
      {
        if (replaceFile && System.IO.File.Exists(fileName))
          System.IO.File.Delete(fileName);
        string directoryName = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(directoryName))
          Directory.CreateDirectory(directoryName);
        using (WebClient client = new WebClient())
        {
          using (cancellationToken.Register(new Action(client.CancelAsync)))
          {
            if (headers != null && headers.Keys.Any<string>())
            {
              foreach (string key in headers.Keys)
                client.Headers.Add(key, headers[key]);
            }
            client.DownloadProgressChanged += (DownloadProgressChangedEventHandler) ((_, args) =>
            {
              Action<ProgressChangedEventArgs> action = progressChangedCallback;
              if (action == null)
                return;
              action((ProgressChangedEventArgs) args);
            });
            await client.DownloadFileTaskAsync(address, fileName).ConfigureAwait(false);
            return new TryResult();
          }
        }
      }
      catch (TaskCanceledException ex)
      {
        return new TryResult();
      }
      catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
      {
        return new TryResult();
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
    }
  }
}
