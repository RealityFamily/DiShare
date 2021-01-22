// Decompiled with JetBrains decompiler
// Type: Logic.Network.Models.FileDownloadState
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

namespace DiShare.Logic.Network.Models
{
  public class FileDownloadState
  {
    public long FileSize { get; }

    public long DownloadedSize { get; }

    public FileDownloadState(long fileSize, long downloadedSize)
    {
      this.FileSize = fileSize;
      this.DownloadedSize = downloadedSize;
    }
  }
}
