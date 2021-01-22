// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Extensions.SubscriptionExtensions
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Api.Subscriptions.Responses;
using DiShare.Domain.Models;

namespace DiShare.Data.Extensions
{
  public static class SubscriptionExtensions
  {
    public static Subscription ToModel(
      this SubscriptionResponse response,
      bool? hasMarathonGroups)
    {
      return new Subscription()
      {
        TotalDrops = response.TotalDrops,
        DailyDrops = response.DailyDrops,
        StartedAt = response.StartedAt,
        ExpiresAt = response.ExpiresAt,
        Used = response.Used,
        Tariff = response.Tariff.ToModel(),
        HasMarathonGroups = hasMarathonGroups.HasValue && hasMarathonGroups.Value
      };
    }
  }
}
