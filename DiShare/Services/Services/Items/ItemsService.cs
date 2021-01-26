

using DiShare.Api.Base;
using DiShare.Api.Items;
using DiShare.Api.Items.Responses;
using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Network;
using DiShare.Logic.Exceptions;
using DiShare.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Items
{
  public class ItemsService : IItemsService
  {
    private readonly IItemsApi _itemsApi;
    private readonly INetworkChecker _networkChecker;

    public ItemsService(IItemsApi itemsApi, INetworkChecker networkChecker)
    {
      this._itemsApi = itemsApi;
      this._networkChecker = networkChecker;
    }

    public async Task<TryResult<IReadOnlyCollection<ItemDto>>> GetAsync(
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this._networkChecker.CheckIsConnected())
        return new TryResult<IReadOnlyCollection<ItemDto>>((Exception) new CategoriesServiceException("Network connection error."));
      try
      {
        Response<IReadOnlyCollection<ItemResponse>> response = await this._itemsApi.GetItemsAsync(token, cancellationToken).ConfigureAwait(false);
        return response.Status == ResponseStatus.Error || !response.Error.IsNullOrEmpty() ? new TryResult<IReadOnlyCollection<ItemDto>>((Exception) new CategoriesServiceException(string.Format("Retrieve categories error: [{0}] {1}", (object) response.Status, (object) response.Error))) : new TryResult<IReadOnlyCollection<ItemDto>>((IReadOnlyCollection<ItemDto>) response.Result.Select<ItemResponse, ItemDto>((Func<ItemResponse, ItemDto>) (i => i.ToDto())).ToArray<ItemDto>());
      }
      catch (Exception ex)
      {
        return new TryResult<IReadOnlyCollection<ItemDto>>((Exception) new CategoriesServiceException("Can't retrieve categories from server", ex));
      }
    }
  }
}
