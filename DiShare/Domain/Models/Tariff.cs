

using System;

namespace DiShare.Domain.Models
{
  public class Tariff
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Decimal Price { get; set; }

    public int Term { get; set; }

    public int? TotalDrops { get; set; }

    public int? DailyDrops { get; set; }

    public bool IsInitial { get; set; }

    protected bool Equals(Tariff other)
    {
      if (this.Id == other.Id && this.Name == other.Name && (this.Description == other.Description && this.Price == other.Price) && this.Term == other.Term)
      {
        int? totalDrops1 = this.TotalDrops;
        int? totalDrops2 = other.TotalDrops;
        if (totalDrops1.GetValueOrDefault() == totalDrops2.GetValueOrDefault() & totalDrops1.HasValue == totalDrops2.HasValue)
        {
          int? dailyDrops1 = this.DailyDrops;
          int? dailyDrops2 = other.DailyDrops;
          if (dailyDrops1.GetValueOrDefault() == dailyDrops2.GetValueOrDefault() & dailyDrops1.HasValue == dailyDrops2.HasValue)
            return this.IsInitial == other.IsInitial;
        }
      }
      return false;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != this.GetType()) && this.Equals((Tariff) obj);
    }

    public override int GetHashCode()
    {
      int num1 = ((((this.Id * 397 ^ (this.Name != null ? this.Name.GetHashCode() : 0)) * 397 ^ (this.Description != null ? this.Description.GetHashCode() : 0)) * 397 ^ this.Price.GetHashCode()) * 397 ^ this.Term) * 397;
      int? nullable = this.TotalDrops;
      int hashCode1 = nullable.GetHashCode();
      int num2 = (num1 ^ hashCode1) * 397;
      nullable = this.DailyDrops;
      int hashCode2 = nullable.GetHashCode();
      return (num2 ^ hashCode2) * 397 ^ this.IsInitial.GetHashCode();
    }
  }
}
