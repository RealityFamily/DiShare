﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Api.ItemDownloaderApi.IItemDownloaderApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Infrastructure;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.ItemDownloaderApi
{
  public interface IItemDownloaderApi
  {
    Task<TryResult> TryDownloadItemAsync(
      string id,
      string path,
      string token,
      Action<ProgressChangedEventArgs> progressChangedCallback = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}