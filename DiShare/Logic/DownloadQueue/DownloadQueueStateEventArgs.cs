// Decompiled with JetBrains decompiler
// Type: Logic.DownloadQueue.DownloadQueueStateEventArgs
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Logic.DownloadQueue.Enums;
using System;

namespace DiShare.Logic.DownloadQueue
{
  public class DownloadQueueStateEventArgs : EventArgs
  {
    public DownloadQueueState State { get; }

    public DownloadQueueStateEventArgs(DownloadQueueState state) => this.State = state;
  }
}
