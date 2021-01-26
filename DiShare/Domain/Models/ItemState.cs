

namespace DiShare.Domain.Models
{
  public enum ItemState
  {
    NotCached = 1,
    Queued = 2,
    Downloading = 3,
    Cached = 4,
    DownloadingError = 5,
    Extraction = 6,
  }
}
