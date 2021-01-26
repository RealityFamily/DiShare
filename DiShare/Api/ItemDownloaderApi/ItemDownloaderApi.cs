
using DiShare.Common;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.ItemDownloaderApi
{
  public class ItemDownloaderApi : IItemDownloaderApi
  {
    private readonly ISimpleFileDownloader _simpleFileDownloader;

    public ItemDownloaderApi(ISimpleFileDownloader simpleFileDownloader) => this._simpleFileDownloader = simpleFileDownloader;

    public Task<TryResult> TryDownloadItemAsync(
      string id,
      string path,
      string token,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "x-api-key",
          ConfigurationTokens.ApiKey
        }
      };
      if (!token.IsNullOrEmpty())
        dictionary.Add("Authorization", "Bearer " + token);
      return this._simpleFileDownloader.TryDownloadFileAsync(ConfigurationTokens.BaseUrl + "api/v1/Items/" + id, path, progressChangedCallback: progressChangedCallback, headers: ((IReadOnlyDictionary<string, string>) dictionary), cancellationToken: cancellationToken);
    }
  }
}
