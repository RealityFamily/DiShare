// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Extensions.VendorExtensions
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

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
