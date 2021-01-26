
using System.ComponentModel;

namespace DiShare.Domain.Models
{
  public class DownloadProgress : INotifyPropertyChanged
  {
    private int progressPercentage;
    private long bytesDownloaded;
    private long fileSize;

    public int ProgressPercentage
    {
      get => this.progressPercentage;
      set
      {
        this.progressPercentage = value;
        this.OnPropertyChanged(nameof (ProgressPercentage));
      }
    }

    public long BytesDownloaded
    {
      get => this.bytesDownloaded;
      set
      {
        this.bytesDownloaded = value;
        this.OnPropertyChanged(nameof (BytesDownloaded));
      }
    }

    public long FileSize
    {
      get => this.fileSize;
      set
      {
        this.fileSize = value;
        this.OnPropertyChanged(nameof (FileSize));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
