

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Items.Responses;
using DiShare.Api.Subscriptions.Responses;
using DiShare.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Items
{
  public class ItemsApi : ApiBase, IItemsApi
  {
    public ItemsApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<IReadOnlyCollection<ItemResponse>>> GetItemsAsync(
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      IParameterRequest parameterRequest = this.Client.GetRequest("api/v1/items").AddHeader("X-API-KEY", this.ApiKey);
      if (!token.IsNullOrWhiteSpace())
        parameterRequest = parameterRequest.AddHeader("Authorization", "Bearer " + token);
      return parameterRequest.ExecuteAsync<Response<IReadOnlyCollection<ItemResponse>>>(cancellationToken);
    }

    public Task<Response<SubscriptionResponse>> PostDropItemAsync(
      string id,
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      IParameterRequest parameterRequest = this.Client.PostRequest("api/v1/items/drop").AddHeader("X-API-KEY", this.ApiKey).AddQueryParameter(nameof (id), id);
      if (!token.IsNullOrWhiteSpace())
        parameterRequest = parameterRequest.AddHeader("Authorization", "Bearer " + token);
      return parameterRequest.ExecuteAsync<Response<SubscriptionResponse>>(cancellationToken);
    }
  }
}
