// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Models.IModelsApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Models.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Models
{
  public interface IModelsApi
  {
    Task<ModelResponse> GetManifestAsync(
      string name,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
