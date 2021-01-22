// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Network.IFileDownloader
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Network
{
  public interface IFileDownloader
  {
    Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
