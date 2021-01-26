

using GalaSoft.MvvmLight;
using DiShare.Data.Providers;
using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Threading;
using DiShare.Logic.VendorFilter.Models;
using DiShare.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Logic.VendorFilter
{
  public class VendorFilter : IVendorFilter
  {
    private const int AllVendorsId = -2;
    private const string AllVendorsName = "Все производители";
    private const string SelectedXVendors = "Выбрано производителей: {0}";
    private static readonly TimeSpan WaitTime = TimeSpan.FromMilliseconds(100.0);
    private readonly IVendorsProvider _vendorsProvider;
    private readonly IDispatcherHelper _dispatcherHelper;
    private readonly MarkVendor _allVendorsMark;
    private readonly AsyncLock _asyncLock;
    private bool _isWaitFilterChangedEventFire;

    public ObservableCollection<MarkVendor> VendorFilterCollection { get; }

    public string FilterTextStatus { get; private set; }

    public VendorFilter(IVendorsProvider vendorsProvider, IDispatcherHelper dispatcherHelper)
    {
      this._vendorsProvider = vendorsProvider;
      this._dispatcherHelper = dispatcherHelper;
      this.VendorFilterCollection = new ObservableCollection<MarkVendor>();
      this._allVendorsMark = new MarkVendor()
      {
        IsChecked = true,
        Vendor = new VendorDto()
        {
          Id = -2,
          Name = "Все производители"
        }
      };
      this._asyncLock = new AsyncLock();
      this.FilterTextStatus = "Все производители";
    }

    public event EventHandler FilterChanged;

    private void MarkVendor_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(sender is MarkVendor markVendor))
        return;
      if (object.Equals((object) markVendor, (object) this._allVendorsMark) && markVendor.IsChecked)
      {
        foreach (MarkVendor markVendor2 in this.VendorFilterCollection.Where<MarkVendor>((Func<MarkVendor, bool>) (w => !w.Equals((object) this._allVendorsMark))))
        {
          if (markVendor2.IsChecked)
            markVendor2.IsChecked = false;
        }
      }
      else if (markVendor.IsChecked)
        this._allVendorsMark.IsChecked = false;
      else if (this.VendorFilterCollection.All<MarkVendor>((Func<MarkVendor, bool>) (w => !w.IsChecked)))
        this._allVendorsMark.IsChecked = true;
      Task.Run(new Func<Task>(this.OnFilterChanged));
    }

    private async Task OnFilterChanged()
    {
      Logic.VendorFilter.VendorFilter vendorFilter = this;
      if (vendorFilter._isWaitFilterChangedEventFire)
        return;
      using (await vendorFilter._asyncLock.LockAsync().ConfigureAwait(false))
      {
        if (vendorFilter._isWaitFilterChangedEventFire)
          return;
        vendorFilter._isWaitFilterChangedEventFire = true;
        await Task.Delay(Logic.VendorFilter.VendorFilter.WaitTime).ConfigureAwait(false);
        if (vendorFilter._allVendorsMark.IsChecked)
        {
          vendorFilter.FilterTextStatus = "Все производители";
        }
        else
        {
          // ISSUE: explicit non-virtual call
          int num = (vendorFilter.VendorFilterCollection).Count<MarkVendor>((Func<MarkVendor, bool>) (w => w.IsChecked));
          vendorFilter.FilterTextStatus = string.Format("Выбрано производителей: {0}", (object) num);
        }
      }
      EventHandler filterChanged = vendorFilter.FilterChanged;
      if (filterChanged != null)
        filterChanged((object) vendorFilter, EventArgs.Empty);
      vendorFilter._isWaitFilterChangedEventFire = false;
    }

    public async Task UpdateVendorsAsync(CancellationToken cancellationToken = default (CancellationToken))
    {
      TryResult<IReadOnlyCollection<VendorDto>> tryResult = await this._vendorsProvider.GetItemsAsync().ConfigureAwait(false);
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        this._dispatcherHelper.CheckBeginInvokeOnUI((Action) (() =>
        {
          MarkVendor[] array = this.VendorFilterCollection.Where<MarkVendor>((Func<MarkVendor, bool>) (w => w.IsChecked)).ToArray<MarkVendor>();
          this.ClearCollectionWithUnsubscribe();
          this._allVendorsMark.PropertyChanged += new PropertyChangedEventHandler(this.MarkVendor_OnPropertyChanged);
          this.VendorFilterCollection.Add(this._allVendorsMark);
          if (tryResult.IsFaulted)
            return;
          foreach (VendorDto vendorDto in (IEnumerable<VendorDto>) tryResult.Value.OrderBy<VendorDto, string>((Func<VendorDto, string>) (w => w.Name)))
          {
            VendorDto vendor = vendorDto;
            bool flag = false;
            if (!this._allVendorsMark.IsChecked)
            {
              MarkVendor markVendor = ((IEnumerable<MarkVendor>) array).FirstOrDefault<MarkVendor>((Func<MarkVendor, bool>) (w => w.Vendor.Equals((object) vendor)));
              flag = markVendor != null && markVendor.IsChecked;
            }
            MarkVendor markVendor1 = new MarkVendor()
            {
              IsChecked = flag,
              Vendor = vendor
            };
            markVendor1.PropertyChanged += new PropertyChangedEventHandler(this.MarkVendor_OnPropertyChanged);
            this.VendorFilterCollection.Add(markVendor1);
          }
        }));
    }

    public bool IsFilterApplied()
    {
      using (this._asyncLock.LockAsync().ConfigureAwait(false).GetAwaiter().GetResult())
        return !this._allVendorsMark.IsChecked && this.VendorFilterCollection.Any<MarkVendor>((Func<MarkVendor, bool>) (w => w.IsChecked && !w.Equals((object) this._allVendorsMark)));
    }

    private void ClearCollectionWithUnsubscribe()
    {
      foreach (ObservableObject vendorFilter in (Collection<MarkVendor>) this.VendorFilterCollection)
        vendorFilter.PropertyChanged -= new PropertyChangedEventHandler(this.MarkVendor_OnPropertyChanged);
      this.VendorFilterCollection.Clear();
    }

    public async Task<IReadOnlyCollection<VendorDto>> GetFilterByVendorCollection()
    {
      Logic.VendorFilter.VendorFilter vendorFilter = this;
      IReadOnlyCollection<VendorDto> array;
      using (await vendorFilter._asyncLock.LockAsync().ConfigureAwait(false))
      {
        // ISSUE: explicit non-virtual call
        // ISSUE: reference to a compiler-generated method
        array = new List<VendorDto>(); //(IReadOnlyCollection<VendorDto>) (vendorFilter.VendorFilterCollection).Where<MarkVendor>(new Func<MarkVendor, bool>(vendorFilter.\u003CGetFilterByVendorCollection\u003Eb__25_0)).Select<MarkVendor, VendorDto>((Func<MarkVendor, VendorDto>) (w => w.Vendor)).ToArray<VendorDto>();
      }
      return array;
    }
  }
}
