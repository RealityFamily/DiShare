
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
