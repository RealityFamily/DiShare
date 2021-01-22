// Decompiled with JetBrains decompiler
// Type: Logic.Subscriptions.Events.DropsChangedEvent
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
