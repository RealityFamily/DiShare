

using DiShare.Domain.Models;
using System;

namespace DiShare.Logic.Subscriptions.Events
{
  public class SubscriptionChangedEvent : EventArgs
  {
    public Subscription Subscription { get; }

    public SubscriptionChangedEvent(Subscription subscription) => this.Subscription = subscription;
  }
}
