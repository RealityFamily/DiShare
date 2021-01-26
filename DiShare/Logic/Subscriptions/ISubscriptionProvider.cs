

using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Logic.Subscriptions.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Logic.Subscriptions
{
  public interface ISubscriptionProvider
  {
    event EventHandler<SubscriptionChangedEvent> OnSubscriptionChanged;

    event EventHandler<DropsChangedEvent> OnDropsChanged;

    Subscription ActiveSubscription { get; }

    int AvailableDrops { get; }

    Task<TryResult<bool>> CanDropAsync();

    void UpdateSubscriptionInfo();

    Task<TryResult<Subscription>> GetSubscriptionAsync();

    Task<TryResult<IEnumerable<Tariff>>> GetTariffsAsync();

    Task<TryResult<SubscribeUrl>> SubscribeAsync(int tarifId);

    int CalcAvailableDrops(Subscription subscription);

    void DropItem(string id);
  }
}
