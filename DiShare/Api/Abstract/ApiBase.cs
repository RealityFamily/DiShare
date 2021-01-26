
using DiShare.Common;
using Tiny.RestClient;

namespace DiShare.Api.Abstract
{
  public abstract class ApiBase
  {
    protected readonly string ApiKey = ConfigurationTokens.ApiKey;
    protected TinyRestClient Client;

    protected ApiBase(TinyRestClient client) => this.Client = client;
  }
}
