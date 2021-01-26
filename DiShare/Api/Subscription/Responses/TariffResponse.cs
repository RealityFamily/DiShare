

using System;

namespace DiShare.Api.Subscriptions.Responses
{
  public class TariffResponse
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Decimal Price { get; set; }

    public int Term { get; set; }

    public int? TotalDrops { get; set; }

    public int? DailyDrops { get; set; }

    public bool IsInitial { get; set; }
  }
}
