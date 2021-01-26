

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Vendors.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Vendors
{
  public class VendorsApi : ApiBase, IVendorsApi
  {
    public VendorsApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<IReadOnlyCollection<VendorResponse>>> GetVendorsAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/vendors").AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<IReadOnlyCollection<VendorResponse>>>(cancellationToken);
    }
  }
}
