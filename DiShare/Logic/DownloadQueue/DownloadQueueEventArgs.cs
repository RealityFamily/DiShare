// Decompiled with JetBrains decompiler
// Type: Logic.DownloadQueue.DownloadQueueEventArgs
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using System;

namespace DiShare.Logic.DownloadQueue
{
  public class DownloadQueueEventArgs : EventArgs
  {
    public int Total { get; }

    public int Completed { get; }

    public DownloadQueueEventArgs(int total, int completed)
    {
      this.Total = total;
      this.Completed = completed;
    }
  }
}
