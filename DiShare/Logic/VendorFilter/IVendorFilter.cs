

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
