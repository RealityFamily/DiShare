

using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Network
{
  public interface IFileDownloader
  {
    Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
