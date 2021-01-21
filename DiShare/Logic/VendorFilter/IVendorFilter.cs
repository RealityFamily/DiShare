// Decompiled with JetBrains decompiler
// Type: Logic.VendorFilter.IVendorFilter
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Domain.DTO;
using DiShare.Logic.VendorFilter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.VendorFilter
{
  public interface IVendorFilter
  {
    ObservableCollection<MarkVendor> VendorFilterCollection { get; }

    string FilterTextStatus { get; }

    Task UpdateVendorsAsync(CancellationToken cancellationToken = default (CancellationToken));

    event EventHandler FilterChanged;

    bool IsFilterApplied();

    Task<IReadOnlyCollection<VendorDto>> GetFilterByVendorCollection();
  }
}
