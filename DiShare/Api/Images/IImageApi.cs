
using DiShare.Domain.Enums;
using DiShare.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Images
{
  public interface IImageApi
  {
    Task<TryResult> TryDownloadImageAsync(
      string id,
      string path,
      ImageSize size,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
