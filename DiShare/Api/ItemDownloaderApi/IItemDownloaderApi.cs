
using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.ItemDownloaderApi
{
  public interface IItemDownloaderApi
  {
    Task<TryResult> TryDownloadItemAsync(
      string id,
      string path,
      string token,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
