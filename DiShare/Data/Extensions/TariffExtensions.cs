// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Extensions.TariffExtensions
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Api.Subscriptions.Responses;
using DiShare.Domain.Models;

namespace DiShare.Data.Extensions
{
  public static class TariffExtensions
  {
    public static Tariff ToModel(this TariffResponse response) => new Tariff()
    {
      Id = response.Id,
      Name = response.Name,
      Description = response.Description,
      TotalDrops = response.TotalDrops,
      DailyDrops = response.DailyDrops,
      Price = response.Price,
      Term = response.Term,
      IsInitial = response.IsInitial
    };
  }
}
