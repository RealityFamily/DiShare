// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Items.ItemsApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
