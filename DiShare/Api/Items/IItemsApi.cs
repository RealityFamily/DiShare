
using DiShare.Api.Base;
using DiShare.Api.Items.Responses;
using DiShare.Api.Subscriptions.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Items
{
  public interface IItemsApi
  {
    Task<Response<IReadOnlyCollection<ItemResponse>>> GetItemsAsync(
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<SubscriptionResponse>> PostDropItemAsync(
      string id,
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
