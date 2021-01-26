

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Infrastructure.Network
{
  public interface ISimpleFileDownloader
  {
    Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      IReadOnlyDictionary<string, string> headers = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
