// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Models.ItemState
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

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
