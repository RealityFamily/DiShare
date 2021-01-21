// Decompiled with JetBrains decompiler
// Type: Logic.Subscriptions.SubscriptionProvider
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Api.Base;
using DiShare.Api.Items;
using DiShare.Api.Subscriptions;
using DiShare.Api.Subscriptions.Responses;
using DiShare.Data.Extensions;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Threading;
using DiShare.Logic.ErrorHandler.Models;
using DiShare.Logic.ExceptionHandler;
using DiShare.Logic.Exceptions;
using DiShare.Logic.Subscriptions.Events;
using DiShare.Logic.Users;
using DiShare.Logic.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace Logic.Subscriptions
{
  public class SubscriptionProvider : ISubscriptionProvider
  {
    private readonly ISubscriptionsApi _subscriptionsApi;
    private readonly IUserProvider _userProvider;
    private readonly IItemsApi _itemsApi;
    private readonly IExceptionHandler _exceptionHandler;
    private readonly AsyncLock _asyncLock;
    private IEnumerable<Tariff> _tariffs;
    private DateTime _tariffsUpdatedAt = DateTime.MinValue;
    private DateTime _subscriptionUpdatedAt = DateTime.MinValue;

    public event EventHandler<SubscriptionChangedEvent> OnSubscriptionChanged;

    public event EventHandler<DropsChangedEvent> OnDropsChanged;

    public Subscription ActiveSubscription { get; private set; }

    public int AvailableDrops { get; private set; }

    public SubscriptionProvider(
      ISubscriptionsApi subscriptionsApi,
      IUserProvider userProvider,
      IItemsApi itemsApi,
      IExceptionHandler exceptionHandler)
    {
      this._subscriptionsApi = subscriptionsApi;
      this._userProvider = userProvider;
      this._itemsApi = itemsApi;
      this._exceptionHandler = exceptionHandler;
      this._asyncLock = new AsyncLock();
      Task.Run(new Action(this.CheckSubscriptionAsync));
    }

    private async void CheckSubscriptionAsync()
    {
      while (true)
      {
        await Task.Delay(TimeSpan.FromHours(1.0));
        TryResult<Subscription> tryResult = await this.GetCachedSubscriptionAsync().ConfigureAwait(false);
      }
    }

    private async Task<TryResult<Subscription>> GetCachedSubscriptionAsync()
    {
      SubscriptionProvider subscriptionProvider = this;
      DateTime now;
      // ISSUE: explicit non-virtual call
      if (!((subscriptionProvider.ActiveSubscription) == (Subscription) null))
      {
        now = DateTime.Now;
        if (!(now.AddHours(-1.0) > subscriptionProvider._subscriptionUpdatedAt))
          goto label_17;
      }
      AsyncLock.LockReleaser lockReleaser = await subscriptionProvider._asyncLock.LockAsync();
      try
      {
        // ISSUE: explicit non-virtual call
        if (!((subscriptionProvider.ActiveSubscription) == (Subscription) null))
        {
          now = DateTime.Now;
          if (!(now.AddHours(-1.0) > subscriptionProvider._subscriptionUpdatedAt))
            goto label_16;
        }
        // ISSUE: explicit non-virtual call
        TryResult<Subscription> tryResult = await (subscriptionProvider.GetSubscriptionAsync()).ConfigureAwait(false);
        if (tryResult.IsFaulted)
          return new TryResult<Subscription>(tryResult.Exception);
        subscriptionProvider.ActiveSubscription = tryResult.Value;
        subscriptionProvider._subscriptionUpdatedAt = DateTime.Now;
        // ISSUE: explicit non-virtual call
        // ISSUE: explicit non-virtual call
        subscriptionProvider.AvailableDrops = (subscriptionProvider.CalcAvailableDrops( (subscriptionProvider.ActiveSubscription)));
        EventHandler<SubscriptionChangedEvent> subscriptionChanged = subscriptionProvider.OnSubscriptionChanged;
        if (subscriptionChanged != null)
        {
          // ISSUE: explicit non-virtual call
          subscriptionChanged((object) subscriptionProvider, new SubscriptionChangedEvent( (subscriptionProvider.ActiveSubscription)));
        }
        EventHandler<DropsChangedEvent> onDropsChanged = subscriptionProvider.OnDropsChanged;
        if (onDropsChanged != null)
        {
          // ISSUE: explicit non-virtual call
          // ISSUE: explicit non-virtual call
          // ISSUE: explicit non-virtual call
          // ISSUE: explicit non-virtual call
          onDropsChanged((object) subscriptionProvider, new DropsChangedEvent( (subscriptionProvider.ActiveSubscription).Used,  (subscriptionProvider.AvailableDrops),  (subscriptionProvider.ActiveSubscription).TotalDrops ??  (subscriptionProvider.ActiveSubscription).DailyDrops.GetValueOrDefault()));
        }
      }
      finally
      {
        ((IDisposable) lockReleaser)?.Dispose();
      }
label_16:
      lockReleaser = (AsyncLock.LockReleaser) null;
label_17:
      // ISSUE: explicit non-virtual call
      return new TryResult<Subscription>( (subscriptionProvider.ActiveSubscription));
    }

    public int CalcAvailableDrops(Subscription subscription) => !subscription.TotalDrops.HasValue ? (!subscription.DailyDrops.HasValue ? 0 : (subscription.DailyDrops.Value > subscription.Used ? subscription.DailyDrops.Value - subscription.Used : 0)) : (subscription.TotalDrops.Value > subscription.Used ? subscription.TotalDrops.Value - subscription.Used : 0);

    public async Task<TryResult<Subscription>> GetSubscriptionAsync()
    {
      TryResult<bool> tryResult = await this._userProvider.HasUserRegisteredAsync().ConfigureAwait(false);
      if (tryResult.IsFaulted)
        return new TryResult<Subscription>(tryResult.Exception);
      if (!tryResult.Value)
        return new TryResult<Subscription>((Exception) new SubscriptionProviderException("User has not registered yet!"));
      TryResult<UserInfo> userInfo = await this._userProvider.GetUserInfoAsync(out TODO).ConfigureAwait(false);
      if (userInfo.IsFaulted)
        return new TryResult<Subscription>(userInfo.Exception);
      try
      {
        Response<SubscriptionResponse> response = await this._subscriptionsApi.GetActiveSubscriptionAsync(userInfo.Value.Token).ConfigureAwait(false);
        return response.Status != ResponseStatus.OK ? new TryResult<Subscription>((Exception) new SubscriptionProviderException("Unknown error happens")) : new TryResult<Subscription>(response.Result.ToModel(userInfo.Value.HasMarathonGroups));
      }
      catch (HttpException ex)
      {
        return ex.StatusCode != HttpStatusCode.Unauthorized ? new TryResult<Subscription>((Exception) new SubscriptionProviderException(string.Format("Unable to retrieve active subscription (Network error = {0})", (object) ex.StatusCode), (Exception) ex)) : new TryResult<Subscription>((Exception) new UnauthorizedUserProviderException("Unable to retrieve active subscription", (Exception) ex));
      }
      catch (Exception ex)
      {
        return new TryResult<Subscription>((Exception) new SubscriptionProviderException("Unable to retrieve active subscription", ex));
      }
    }

    public async Task<TryResult<bool>> CanDropAsync()
    {
      TryResult<Subscription> tryResult = await this.GetCachedSubscriptionAsync().ConfigureAwait(false);
      return !tryResult.IsFaulted ? new TryResult<bool>(tryResult.Value.TotalDrops.HasValue && tryResult.Value.TotalDrops.Value > tryResult.Value.Used || tryResult.Value.DailyDrops.HasValue && tryResult.Value.DailyDrops.Value > tryResult.Value.Used || tryResult.Value.HasMarathonGroups) : new TryResult<bool>(tryResult.Exception);
    }

    public async void UpdateSubscriptionInfo()
    {
      SubscriptionProvider subscriptionProvider = this;
      TryResult<UserInfo> tryResult = await subscriptionProvider._userProvider.UpdateUserInfoAsync().ConfigureAwait(false);
      subscriptionProvider.ActiveSubscription = (Subscription) null;
      Task.Run<TryResult<Subscription>>(new Func<Task<TryResult<Subscription>>>(subscriptionProvider.GetCachedSubscriptionAsync));
    }

    public async Task<TryResult<IEnumerable<Tariff>>> GetTariffsAsync()
    {
      if (this._tariffs == null || this._tariffsUpdatedAt >= DateTime.UtcNow.AddHours(-1.0))
      {
        AsyncLock.LockReleaser lockReleaser = await this._asyncLock.LockAsync();
        try
        {
          int num;
          if (num != 1 && this._tariffs != null)
          {
            if (!(this._tariffsUpdatedAt >= DateTime.UtcNow.AddHours(-1.0)))
              goto label_13;
          }
          try
          {
            Response<IReadOnlyCollection<TariffResponse>> response = await this._subscriptionsApi.GetTariffsAsync().ConfigureAwait(false);
            if (response.Status != ResponseStatus.OK)
              return new TryResult<IEnumerable<Tariff>>((Exception) new SubscriptionProviderException("Unable to retrieve actual tariffs. Please try again later. Error = " + response.Error));
            this._tariffs = (IEnumerable<Tariff>) response.Result.Select<TariffResponse, Tariff>((Func<TariffResponse, Tariff>) (t => t.ToModel())).OrderBy<Tariff, Decimal>((Func<Tariff, Decimal>) (t => t.Price)).ToArray<Tariff>();
            this._tariffsUpdatedAt = DateTime.UtcNow;
          }
          catch (Exception ex)
          {
            return new TryResult<IEnumerable<Tariff>>((Exception) new SubscriptionProviderException("Unable to retrieve actual tariffs. Please try again later.", ex));
          }
        }
        finally
        {
          ((IDisposable) lockReleaser)?.Dispose();
        }
label_13:
        lockReleaser = (AsyncLock.LockReleaser) null;
      }
      return new TryResult<IEnumerable<Tariff>>(this._tariffs);
    }

    public async Task<TryResult<SubscribeUrl>> SubscribeAsync(int tarifId)
    {
      try
      {
        TryResult<UserInfo> tryResult = await this._userProvider.GetUserInfoAsync(out TODO).ConfigureAwait(false);
        if (tryResult.IsFaulted)
          return new TryResult<SubscribeUrl>((Exception) new SubscriptionProviderException("You need to register before subscribing"));
        Response<SubscribeResponse> response = await this._subscriptionsApi.SubscribeAsync(tryResult.Value.Token, tarifId).ConfigureAwait(false);
        if (response.Status != ResponseStatus.OK)
          return new TryResult<SubscribeUrl>((Exception) new SubscriptionProviderException("Unable to checkout. Please try again later. Error = " + response.Error));
        return (TryResult<SubscribeUrl>) new SubscribeUrl()
        {
          Paid = response.Result.Paid,
          ReturnUrl = response.Result.ReturnUrl
        };
      }
      catch (Exception ex)
      {
        return new TryResult<SubscribeUrl>((Exception) new SubscriptionProviderException("Unable to checkout. Please try again later.", ex));
      }
    }

    private void UpdateAvailableDrops()
    {
      int num = this.ActiveSubscription.TotalDrops.HasValue ? this.ActiveSubscription.TotalDrops.Value - this.ActiveSubscription.Used : (this.ActiveSubscription.DailyDrops.HasValue ? this.ActiveSubscription.DailyDrops.Value - this.ActiveSubscription.Used : 0);
      this.AvailableDrops = num >= 0 ? num : 0;
      EventHandler<DropsChangedEvent> onDropsChanged = this.OnDropsChanged;
      if (onDropsChanged == null)
        return;
      onDropsChanged((object) this, new DropsChangedEvent(this.ActiveSubscription.Used, this.AvailableDrops, this.ActiveSubscription.TotalDrops ?? this.ActiveSubscription.DailyDrops.GetValueOrDefault()));
    }

    public async void DropItem(string id)
    {
      SubscriptionProvider subscriptionProvider = this;
      TryResult<UserInfo> userInfoResult = await subscriptionProvider._userProvider.GetUserInfoAsync(out TODO).ConfigureAwait(false);
      string token = userInfoResult.IsFaulted ? string.Empty : userInfoResult.Value.Token;
      UserInfo userInfo = userInfoResult.Value;
      bool? nullable1;
      bool? nullable2;
      if (userInfo == null)
      {
        nullable2 = new bool?();
      }
      else
      {
        nullable1 = userInfo.HasMarathonGroups;
        nullable2 = nullable1.HasValue ? new bool?(!nullable1.GetValueOrDefault()) : new bool?();
      }
      nullable1 = nullable2;
      // ISSUE: explicit non-virtual call
      if (nullable1.GetValueOrDefault() &&(subscriptionProvider.AvailableDrops) > 0)
      {
        using (await subscriptionProvider._asyncLock.LockAsync())
        {
          // ISSUE: explicit non-virtual call
          if ( (subscriptionProvider.AvailableDrops) > 0)
          {
            // ISSUE: explicit non-virtual call
            ++ (subscriptionProvider.ActiveSubscription).Used;
          }
          subscriptionProvider.UpdateAvailableDrops();
        }
      }
      try
      {
        Response<SubscriptionResponse> response = await subscriptionProvider._itemsApi.PostDropItemAsync(id, token);
        if (response.Status == ResponseStatus.Error)
          throw new SubscriptionProviderException("Unable to post drop event. Error = " + response.Error);
        Subscription subscription = response.Result.ToModel(userInfoResult.Value.HasMarathonGroups);
        // ISSUE: explicit non-virtual call
        if ( (subscriptionProvider.ActiveSubscription).Used != subscription.Used)
        {
          // ISSUE: explicit non-virtual call
          int? nullable3 =  (subscriptionProvider.ActiveSubscription).TotalDrops;
          int? totalDrops = subscription.TotalDrops;
          if (nullable3.GetValueOrDefault() == totalDrops.GetValueOrDefault() & nullable3.HasValue == totalDrops.HasValue)
          {
            // ISSUE: explicit non-virtual call
            int? dailyDrops =(subscriptionProvider.ActiveSubscription).DailyDrops;
            nullable3 = subscription.DailyDrops;
            if (dailyDrops.GetValueOrDefault() == nullable3.GetValueOrDefault() & dailyDrops.HasValue == nullable3.HasValue)
              goto label_26;
          }
          using (await subscriptionProvider._asyncLock.LockAsync())
          {
            // ISSUE: explicit non-virtual call
             (subscriptionProvider.ActiveSubscription).Tariff = subscription.Tariff;
            // ISSUE: explicit non-virtual call
             (subscriptionProvider.ActiveSubscription).DailyDrops = subscription.DailyDrops;
            // ISSUE: explicit non-virtual call
             (subscriptionProvider.ActiveSubscription).TotalDrops = subscription.TotalDrops;
            // ISSUE: explicit non-virtual call
             (subscriptionProvider.ActiveSubscription).ExpiresAt = subscription.ExpiresAt;
            // ISSUE: explicit non-virtual call
             (subscriptionProvider.ActiveSubscription).StartedAt = subscription.StartedAt;
            subscriptionProvider._subscriptionUpdatedAt = DateTime.Now;
            EventHandler<SubscriptionChangedEvent> subscriptionChanged = subscriptionProvider.OnSubscriptionChanged;
            if (subscriptionChanged != null)
              subscriptionChanged((object) subscriptionProvider, new SubscriptionChangedEvent(subscription));
          }
        }
label_26:
        subscriptionProvider.UpdateAvailableDrops();
        subscription = (Subscription) null;
      }
      catch (Exception ex)
      {
        subscriptionProvider._exceptionHandler.HandleException(ex, notificationMode: NotificationMode.Suppress);
        subscriptionProvider.AvailableDrops = 0;
        using (await subscriptionProvider._asyncLock.LockAsync())
        {
          // ISSUE: explicit non-virtual call
           (subscriptionProvider.ActiveSubscription).DailyDrops = new int?(0);
          // ISSUE: explicit non-virtual call
           (subscriptionProvider.ActiveSubscription).TotalDrops = new int?(0);
          // ISSUE: explicit non-virtual call
           (subscriptionProvider.ActiveSubscription).Used = 0;
        }
        subscriptionProvider.UpdateAvailableDrops();
      }
    }
  }
}
