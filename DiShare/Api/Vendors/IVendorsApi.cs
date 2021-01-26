

using DiShare.Api.Base;
using DiShare.Api.Vendors.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Vendors
{
  public interface IVendorsApi
  {
    Task<Response<IReadOnlyCollection<VendorResponse>>> GetVendorsAsync(
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
