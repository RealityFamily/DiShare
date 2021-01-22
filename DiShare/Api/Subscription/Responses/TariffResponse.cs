// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Subscriptions.Responses.TariffResponse
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
