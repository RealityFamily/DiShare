// Decompiled with JetBrains decompiler
// Type: Library.Logic.Subscriptions.ISubscriptionProvider
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Logic.dll

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
