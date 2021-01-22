// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Subscriptions.SubscriptionsApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Subscriptions.Responses;
using DiShare.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Subscriptions
{
  public class SubscriptionsApi : ApiBase, ISubscriptionsApi
  {
    public SubscriptionsApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<SubscriptionResponse>> GetActiveSubscriptionAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      IParameterRequest parameterRequest = this.Client.GetRequest("api/v1/subscriptions").AddHeader("X-API-KEY", this.ApiKey);
      if (!token.IsNullOrWhiteSpace())
        parameterRequest = parameterRequest.AddHeader("Authorization", "Bearer " + token);
      return parameterRequest.ExecuteAsync<Response<SubscriptionResponse>>(cancellationToken);
    }

    public Task<Response<SubscribeResponse>> SubscribeAsync(
      string token,
      int tarifId,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      IParameterRequest parameterRequest = this.Client.PostRequest(string.Format("api/v1/subscriptions/subscribe/{0}", (object) tarifId)).AddHeader("X-API-KEY", this.ApiKey);
      if (!token.IsNullOrWhiteSpace())
        parameterRequest = parameterRequest.AddHeader("Authorization", "Bearer " + token);
      return parameterRequest.ExecuteAsync<Response<SubscribeResponse>>(cancellationToken);
    }

    public Task<Response<IReadOnlyCollection<TariffResponse>>> GetTariffsAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/subscriptions/tariffs").AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<IReadOnlyCollection<TariffResponse>>>(cancellationToken);
    }
  }
}
