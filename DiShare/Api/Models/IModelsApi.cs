

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
