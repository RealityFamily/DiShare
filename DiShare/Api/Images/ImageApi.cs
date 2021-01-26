
using DiShare.Common;
using DiShare.Domain.Enums;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Network;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Images
{
  public class ImageApi : IImageApi
  {
    private readonly ISimpleFileDownloader _simpleFileDownloader;

    public ImageApi(ISimpleFileDownloader simpleFileDownloader) => this._simpleFileDownloader = simpleFileDownloader;

    public Task<TryResult> TryDownloadImageAsync(
      string id,
      string path,
      ImageSize size,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      ISimpleFileDownloader simpleFileDownloader = this._simpleFileDownloader;
      string address = ConfigurationTokens.BaseUrl + string.Format("api/v1/Items/{0}/Image?size={1}", (object) id, (object) size);
      string fileName = path;
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      dictionary.Add("x-api-key", ConfigurationTokens.ApiKey);
      CancellationToken cancellationToken1 = cancellationToken;
      return simpleFileDownloader.TryDownloadFileAsync(address, fileName, headers: ((IReadOnlyDictionary<string, string>) dictionary), cancellationToken: cancellationToken1);
    }
  }
}
