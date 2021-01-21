﻿// Decompiled with JetBrains decompiler
// Type: Logic.VendorFilter.Models.MarkVendor
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using GalaSoft.MvvmLight;
using DiShare.Domain.DTO;

namespace DiShare.Logic.VendorFilter.Models
{
  public class MarkVendor : ObservableObject
  {
    private VendorDto _vendor;
    private bool _isChecked;

    public VendorDto Vendor
    {
      get => this._vendor;
      set => this.Set<VendorDto>(ref this._vendor, value, nameof (Vendor));
    }

    public bool IsChecked
    {
      get => this._isChecked;
      set => this.Set<bool>(ref this._isChecked, value, nameof (IsChecked));
    }

    public override bool Equals(object otherObj)
    {
      if (otherObj == null)
        return false;
      if (this == otherObj)
        return true;
      return !(otherObj.GetType() != this.GetType()) && this.Equals((MarkVendor) otherObj);
    }

    protected bool Equals(MarkVendor other) => object.Equals((object) this._vendor, (object) other._vendor) && this._isChecked == other._isChecked;

    public override int GetHashCode() => (this._vendor != null ? this._vendor.GetHashCode() : 0) * 397 ^ this._isChecked.GetHashCode();
  }
}