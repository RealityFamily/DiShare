// Decompiled with JetBrains decompiler
// Type: DiShare.Infrastructure.Network.ISimpleFileDownloader
// Assembly: DiShare.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Infrastructure.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Infrastructure.Network
{
  public interface ISimpleFileDownloader
  {
    Task<TryResult> TryDownloadFileAsync(
      string address,
      string fileName,
      bool replaceFile = true,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      IReadOnlyDictionary<string, string> headers = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
