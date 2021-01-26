

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
