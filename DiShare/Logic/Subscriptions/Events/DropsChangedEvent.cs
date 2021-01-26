

using System;

namespace DiShare.Logic.Subscriptions.Events
{
  public class DropsChangedEvent : EventArgs
  {
    public int UsedDrops { get; }

    public int AvailableDrops { get; set; }

    public int Drops { get; set; }

    public DropsChangedEvent(int used, int available, int drops)
    {
      this.UsedDrops = used;
      this.AvailableDrops = available;
      this.Drops = drops;
    }
  }
}
