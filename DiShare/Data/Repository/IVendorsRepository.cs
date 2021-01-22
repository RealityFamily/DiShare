// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Repository.IVendorsRepository
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

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
