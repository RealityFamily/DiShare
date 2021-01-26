

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
