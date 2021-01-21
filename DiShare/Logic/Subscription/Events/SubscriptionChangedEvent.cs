// Decompiled with JetBrains decompiler
// Type: Logic.Subscriptions.Events.SubscriptionChangedEvent
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
