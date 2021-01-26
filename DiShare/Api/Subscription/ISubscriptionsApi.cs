

using DiShare.Api.Base;
using DiShare.Api.Subscriptions.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Subscriptions
{
  public interface ISubscriptionsApi
  {
    Task<Response<SubscriptionResponse>> GetActiveSubscriptionAsync(
      string token,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<SubscribeResponse>> SubscribeAsync(
      string token,
      int tarifId,
      CancellationToken cancellationToken = default (CancellationToken));

    Task<Response<IReadOnlyCollection<TariffResponse>>> GetTariffsAsync(
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
