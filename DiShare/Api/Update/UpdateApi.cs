

using DiShare.Api.Abstract;
using DiShare.Api.Update.Responses;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Update
{
  public class UpdateApi : ApiBase, IUpdateApi
  {
    public UpdateApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<UpdateResponse> GetUpdateInfoAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/update").AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<UpdateResponse>(cancellationToken);
    }
  }
}
