

using DiShare.Domain.DTO;
using DiShare.Domain.Entities;

namespace DiShare.Data.Extensions
{
  public static class VendorExtensions
  {
    public static VendorDto ToDto(this Vendor item) => new VendorDto()
    {
      Id = item.Id,
      Name = item.Name,
      Url = item.Url
    };

    public static Vendor ToEntity(this VendorDto item) => new Vendor()
    {
      Id = item.Id,
      Name = item.Name,
      Url = item.Url
    };
  }
}
