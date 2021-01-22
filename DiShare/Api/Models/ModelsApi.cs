// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Models.ModelsApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Abstract;
using DiShare.Api.Models.Responses;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Models
{
  public class ModelsApi : ApiBase, IModelsApi
  {
    public ModelsApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<ModelResponse> GetManifestAsync(
      string name,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("updates/" + name + ".json").ExecuteAsync<ModelResponse>(cancellationToken);
    }
  }
}
