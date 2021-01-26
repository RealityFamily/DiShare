

using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Vendors
{
  public interface IVendorsService
  {
    Task<TryResult<IReadOnlyCollection<VendorDto>>> GetAsync(
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
