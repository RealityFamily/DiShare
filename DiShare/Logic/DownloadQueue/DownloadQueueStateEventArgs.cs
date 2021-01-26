

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
