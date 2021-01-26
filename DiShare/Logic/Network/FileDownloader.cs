

using DiShare.Infrastructure;
using DiShare.Logic.Network.Models;
using DiShare.Services.Network;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.Network
{
  public class FileDownloader : IFileDownloader
  {
    private static readonly int bufferSize = 1024000;
    private FileStream saveFileStream;
    private Stream responseStream;

    public async Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      object obj = (object) null;
      int num = 0;
      TryResult tryResult;
      try
      {
        try
        {
          long downloadedSize = 0;
          long fileSize = 0;
          if (System.IO.File.Exists(fileName))
          {
            if (replaceFile)
            {
              System.IO.File.Delete(fileName);
              this.saveFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
              downloadedSize = new FileInfo(fileName).Length;
              this.saveFileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
          }
          else
            this.saveFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
          try
          {
            fileSize = await this.GetFileSize(address);
            if (downloadedSize >= fileSize)
            {
              tryResult = new TryResult();
              goto label_24;
            }
          }
          catch (WebException ex)
          {
            tryResult = new TryResult((Exception) ex);
            goto label_24;
          }
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(address);
          httpWebRequest.AddRange(downloadedSize);
          HttpWebResponse responseAsync = (HttpWebResponse) await httpWebRequest.GetResponseAsync();
          this.responseStream = responseAsync.GetResponseStream();
          long downloadLenght = responseAsync.ContentLength;
          byte[] downBuffer = new byte[FileDownloader.bufferSize];
          while (true)
          {
            Action<ProgressChangedEventArgs> action;
            do
            {
              do
              {
                int downloaded;
                if ((downloaded = await this.responseStream.ReadAsync(downBuffer, 0, downBuffer.Length, cancellationToken)) > 0)
                {
                  await this.saveFileStream.WriteAsync(downBuffer, 0, downloaded, cancellationToken).ConfigureAwait(false);
                  downloadedSize += (long) downloaded;
                }
                else
                  goto label_19;
              }
              while (downloadLenght <= 0L);
              action = progressChangedCallback;
            }
            while (action == null);
            action(new ProgressChangedEventArgs((int) ((double) downloadedSize / (double) fileSize * 100.0), (object) new FileDownloadState(fileSize, downloadedSize)));
          }
label_19:
          tryResult = new TryResult();
        }
        catch (TaskCanceledException ex)
        {
          tryResult = new TryResult();
        }
        catch (WebException ex) when (ex.Status == WebExceptionStatus.RequestCanceled)
        {
          tryResult = new TryResult();
        }
        catch (Exception ex)
        {
          tryResult = new TryResult(ex);
        }
label_24:
        num = 1;
      }
      catch (Exception ex)
      {
        obj = ex;
      }
      await this.saveFileStream?.FlushAsync();
      this.saveFileStream?.Close();
      this.responseStream?.Close();
      object obj1 = obj;
      if (obj1 != null)
      {
        if (!(obj1 is Exception source))
          throw (Exception)obj1;
        ExceptionDispatchInfo.Capture(source).Throw();
      }
      if (num == 1)
        return tryResult = new TryResult();// что-то не так зареверсилось. Исправил костылем
      obj = (object) null;
      tryResult = new TryResult();
      return tryResult;
    }

    private async Task<long> GetFileSize(string url)
    {
      long result = -1;
      WebRequest webRequest = WebRequest.Create(url);
      webRequest.Method = "HEAD";
      using (WebResponse webResponse = await webRequest.GetResponseAsync().ConfigureAwait(false))
      {
        long result1;
        if (long.TryParse(webResponse.Headers.Get("Content-Length"), out result1))
          result = result1;
      }
      return result;
    }
  }
}
