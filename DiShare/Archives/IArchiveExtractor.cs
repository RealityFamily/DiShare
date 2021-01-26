

using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Archives
{
  public interface IArchiveExtractor
  {
    Task<TryResult<bool>> ExtractAsync(
      string archive,
      string folder,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
