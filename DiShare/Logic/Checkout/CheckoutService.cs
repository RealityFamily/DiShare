// Decompiled with JetBrains decompiler
// Type: Logic.Checkout.CheckoutService
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Analytics;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Logic.ShellExecutor;
using DiShare.Logic.Subscriptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.Checkout
{
  public class CheckoutService : ICheckoutService
  {
    private readonly IShellExecutor _shellExecutor;
    private readonly IAnalyticsTracker _analyticsTracker;
    private readonly ISubscriptionProvider _subscriptionProvider;

    public CheckoutService(
      IShellExecutor shellExecutor,
      IAnalyticsTracker analyticsTracker,
      ISubscriptionProvider subscriptionProvider)
    {
      this._shellExecutor = shellExecutor;
      this._analyticsTracker = analyticsTracker;
      this._subscriptionProvider = subscriptionProvider;
    }

    public async Task<TryResult<bool>> CheckoutAsync(
      int tariffId,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      Subscription subscription = this._subscriptionProvider.ActiveSubscription;
      if (subscription == (Subscription) null)
      {
        TryResult<Subscription> tryResult = await this._subscriptionProvider.GetSubscriptionAsync().ConfigureAwait(false);
        if (tryResult.IsFaulted)
          return new TryResult<bool>(tryResult.Exception);
        subscription = tryResult.Value;
      }
      TryResult<SubscribeUrl> tryResult1 = await this._subscriptionProvider.SubscribeAsync(tariffId).ConfigureAwait(false);
      if (tryResult1.IsFaulted)
        return new TryResult<bool>(tryResult1.Exception);
      if (tryResult1.Value.Paid)
      {
        TryResult<Subscription> tryResult2 = await this._subscriptionProvider.GetSubscriptionAsync().ConfigureAwait(false);
        return !tryResult2.IsFaulted ? (TryResult<bool>) !tryResult2.Value.Equals((object) subscription) : new TryResult<bool>(tryResult2.Exception);
      }
      this._shellExecutor.Execute(tryResult1.Value.ReturnUrl);
      try
      {
        TryResult<Subscription> tryResult2;
        while (true)
        {
          if (!cancellationToken.IsCancellationRequested)
          {
            tryResult2 = await this._subscriptionProvider.GetSubscriptionAsync().ConfigureAwait(false);
            if (!tryResult2.IsFaulted)
            {
              if (tryResult2.Value.Equals((object) subscription))
                await Task.Delay(TimeSpan.FromSeconds(5.0), cancellationToken).ConfigureAwait(false);
              else
                goto label_17;
            }
            else
              goto label_14;
          }
          else
            break;
        }
        this._analyticsTracker.SendEvent("InApp", "CheckOutCancelled", tariffId.ToString());
        return new TryResult<bool>(false);
label_14:
        return new TryResult<bool>(tryResult2.Exception);
label_17:
        this._analyticsTracker.SendEvent("InApp", "CheckOutCompleted", tariffId.ToString());
      }
      catch (TaskCanceledException ex)
      {
        this._analyticsTracker.SendEvent("InApp", "CheckOutCancelled", tariffId.ToString());
        return new TryResult<bool>(false);
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
      this._subscriptionProvider.UpdateSubscriptionInfo();
      return new TryResult<bool>(true);
    }
  }
}
