

using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data.Providers
{
  public interface IVendorsProvider
  {
    Task InitCacheAsync();

    Task<TryResult<IReadOnlyCollection<VendorDto>>> GetItemsAsync();
  }
}
