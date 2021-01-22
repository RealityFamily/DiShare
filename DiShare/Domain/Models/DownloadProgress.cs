// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Models.DownloadProgress
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

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
