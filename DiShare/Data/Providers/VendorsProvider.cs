

using DiShare.Data.Extensions;
using DiShare.Data.Repository;
using DiShare.Domain.DTO;
using DiShare.Domain.Entities;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Threading;
using DiShare.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DiShare.Data.Providers
{
  public class VendorsProvider : IVendorsProvider
  {
    private readonly IVendorsRepository _vendorsRepository;
    private readonly IVendorsService _vendorsService;
    private readonly List<VendorDto> _vendors = new List<VendorDto>();
    private readonly AsyncLock _asyncLock;

    public VendorsProvider(IVendorsRepository vendorsRepository, IVendorsService vendorsService)
    {
      this._vendorsRepository = vendorsRepository;
      this._vendorsService = vendorsService;
      this._asyncLock = new AsyncLock();
    }

    public async Task InitCacheAsync()
    {
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        this.AddVendors((IReadOnlyCollection<VendorDto>) (await this._vendorsRepository.GetVendorsAsync().ConfigureAwait(false)).Select<Vendor, VendorDto>((Func<Vendor, VendorDto>) (i => i.ToDto())).ToArray<VendorDto>());
        TryResult<IReadOnlyCollection<VendorDto>> tryResult = await this._vendorsService.GetAsync().ConfigureAwait(false);
        if (!tryResult.IsFaulted)
          await this.UpdateCacheAsync(tryResult.Value).ConfigureAwait(false);
      }
    }

    private void AddVendors(IReadOnlyCollection<VendorDto> items)
    {
      foreach (VendorDto vendorDto in (IEnumerable<VendorDto>) items)
      {
        if (!this._vendors.Contains(vendorDto))
          this._vendors.Add(vendorDto);
      }
    }

    private async Task AddVendorAsync(VendorDto vendor)
    {
      if (this._vendors.Contains(vendor))
        return;
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        if (this._vendors.Contains(vendor))
          return;
        this._vendors.Add(vendor);
        try
        {
          if (await this._vendorsRepository.GetVendorByIdAsync(vendor.Id).ConfigureAwait(false) == null)
            await this._vendorsRepository.AddVendorAsync(vendor.ToEntity()).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
        }
      }
    }

    private async Task UpdateCacheAsync(IReadOnlyCollection<VendorDto> items)
    {
      try
      {
        if (!items.Any<VendorDto>())
          return;
        Vendor[] cachedVendors = (await this._vendorsRepository.GetVendorsAsync().ConfigureAwait(false)).ToArray<Vendor>();
        int[] newIds = items.Select<VendorDto, int>((Func<VendorDto, int>) (i => i.Id)).ToArray<int>();
        int[] numArray = ((IEnumerable<Vendor>) cachedVendors).Where<Vendor>((Func<Vendor, bool>) (i => !((IEnumerable<int>) newIds).Contains<int>(i.Id))).Select<Vendor, int>((Func<Vendor, int>) (i => i.Id)).ToArray<int>();
        int index;
        for (index = 0; index < numArray.Length; ++index)
          await this._vendorsRepository.DeleteVendorAsync(numArray[index]).ConfigureAwait(false);
        numArray = (int[]) null;
        Vendor[] vendorArray = ((IEnumerable<Vendor>) cachedVendors).Where<Vendor>((Func<Vendor, bool>) (i => ((IEnumerable<int>) newIds).Contains<int>(i.Id))).ToArray<Vendor>();
        ConfiguredTaskAwaitable configuredTaskAwaitable;
        for (index = 0; index < vendorArray.Length; ++index)
        {
          Vendor item = vendorArray[index];
          VendorDto vendorDto = items.FirstOrDefault<VendorDto>((Func<VendorDto, bool>) (i => i.Id == item.Id));
          if (vendorDto != null)
          {
            Vendor entity = vendorDto.ToEntity();
            if (!entity.Equals((object) item))
            {
              configuredTaskAwaitable = this._vendorsRepository.UpdateVendorAsync(item.Id, entity).ConfigureAwait(false);
              await configuredTaskAwaitable;
            }
          }
        }
        vendorArray = (Vendor[]) null;
        int[] cachedIds = ((IEnumerable<Vendor>) cachedVendors).Select<Vendor, int>((Func<Vendor, int>) (i => i.Id)).ToArray<int>();
        VendorDto[] vendorDtoArray = items.Where<VendorDto>((Func<VendorDto, bool>) (i => !((IEnumerable<int>) cachedIds).Contains<int>(i.Id))).ToArray<VendorDto>();
        for (index = 0; index < vendorDtoArray.Length; ++index)
        {
          configuredTaskAwaitable = this._vendorsRepository.AddVendorAsync(vendorDtoArray[index].ToEntity()).ConfigureAwait(false);
          await configuredTaskAwaitable;
        }
        vendorDtoArray = (VendorDto[]) null;
        this._vendors.Clear();
        this._vendors.AddRange((IEnumerable<VendorDto>) items);
        cachedVendors = (Vendor[]) null;
      }
      catch (Exception ex)
      {
      }
    }

    public async Task<TryResult<IReadOnlyCollection<VendorDto>>> GetItemsAsync()
    {
      if (!this._vendors.Any<VendorDto>())
      {
        using (await this._asyncLock.LockAsync())
        {
          if (!this._vendors.Any<VendorDto>())
            this._vendors.AddRange((await this._vendorsRepository.GetVendorsAsync().ConfigureAwait(false)).Select<Vendor, VendorDto>((Func<Vendor, VendorDto>) (v => v.ToDto())));
        }
      }
      //return (TryResult<IReadOnlyCollection<VendorDto>>) (IReadOnlyCollection<VendorDto>) this._vendors;
      return this._vendors;
    }
  }
}
