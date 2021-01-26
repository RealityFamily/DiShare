

using DiShare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data.Repository
{
  public interface IVendorsRepository
  {
    Task<IEnumerable<Vendor>> GetVendorsAsync();

    Task<Vendor> GetVendorByIdAsync(int id);

    Task AddVendorAsync(Vendor vendor);

    Task UpdateVendorAsync(int id, Vendor vendor);

    Task DeleteVendorAsync(int id);
  }
}
