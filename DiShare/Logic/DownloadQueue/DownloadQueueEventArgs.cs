

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
