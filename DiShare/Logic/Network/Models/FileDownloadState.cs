

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
