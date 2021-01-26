

using System;

namespace DiShare.Domain.Models
{
  public class Subscription
  {
    public DateTime StartedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public Tariff Tariff { get; set; }

    public int Used { get; set; }

    public int? TotalDrops { get; set; }

    public int? DailyDrops { get; set; }

    public bool HasMarathonGroups { get; set; }

    protected bool Equals(Subscription other)
    {
      if (this.StartedAt.Equals(other.StartedAt) && (this.ExpiresAt.Equals(other.ExpiresAt) && object.Equals((object) this.Tariff, (object) other.Tariff) && this.Used == other.Used))
      {
        int? totalDrops1 = this.TotalDrops;
        int? totalDrops2 = other.TotalDrops;
        if (totalDrops1.GetValueOrDefault() == totalDrops2.GetValueOrDefault() & totalDrops1.HasValue == totalDrops2.HasValue)
        {
          int? dailyDrops1 = this.DailyDrops;
          int? dailyDrops2 = other.DailyDrops;
          if (dailyDrops1.GetValueOrDefault() == dailyDrops2.GetValueOrDefault() & dailyDrops1.HasValue == dailyDrops2.HasValue)
            return this.HasMarathonGroups == other.HasMarathonGroups;
        }
      }
      return false;
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return !(obj.GetType() != this.GetType()) && this.Equals((Subscription) obj);
    }

    public override int GetHashCode()
    {
      DateTime dateTime = this.StartedAt;
      int num = dateTime.GetHashCode() * 397;
      dateTime = this.ExpiresAt;
      int hashCode = dateTime.GetHashCode();
      return (((((num ^ hashCode) * 397 ^ (this.Tariff != null ? this.Tariff.GetHashCode() : 0)) * 397 ^ this.Used) * 397 ^ this.TotalDrops.GetHashCode()) * 397 ^ this.DailyDrops.GetHashCode()) * 397 ^ this.HasMarathonGroups.GetHashCode();
    }

    public static bool operator ==(Subscription left, Subscription right) => object.Equals((object) left, (object) right);

    public static bool operator !=(Subscription left, Subscription right) => !object.Equals((object) left, (object) right);
  }
}
