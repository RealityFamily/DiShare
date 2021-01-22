// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Vendors.IVendorsService
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

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
