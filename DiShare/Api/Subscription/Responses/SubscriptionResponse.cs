// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Subscriptions.Responses.SubscriptionResponse
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
