

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
