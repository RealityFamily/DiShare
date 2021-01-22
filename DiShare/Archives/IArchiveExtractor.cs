// Decompiled with JetBrains decompiler
// Type: DiShare.Archives.IArchiveExtractor
// Assembly: DiShare.Archives, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A03294E2-42F4-4728-80C5-334A63763BEF
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Archives.dll

using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Archives
{
  public interface IArchiveExtractor
  {
    Task<TryResult<bool>> ExtractAsync(
      string archive,
      string folder,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
