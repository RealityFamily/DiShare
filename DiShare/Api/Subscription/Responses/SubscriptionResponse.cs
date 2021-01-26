
using System;

namespace DiShare.Api.Subscriptions.Responses
{
  public class SubscriptionResponse
  {
    public DateTime StartedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public TariffResponse Tariff { get; set; }

    public int Used { get; set; }

    public int? TotalDrops { get; set; }

    public int? DailyDrops { get; set; }
  }
}
