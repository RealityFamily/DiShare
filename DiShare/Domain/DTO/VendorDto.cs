﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.DTO.VendorDto
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.DTO
{
  public class VendorDto
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != this.GetType()) && this.Equals((VendorDto) obj);
    }

    protected bool Equals(VendorDto other) => this.Id == other.Id && string.Equals(this.Name, other.Name, StringComparison.InvariantCulture) && string.Equals(this.Url, other.Url, StringComparison.InvariantCulture);

    public override int GetHashCode() => (this.Id * 397 ^ (this.Name != null ? StringComparer.InvariantCulture.GetHashCode(this.Name) : 0)) * 397 ^ (this.Url != null ? StringComparer.InvariantCulture.GetHashCode(this.Url) : 0);
  }
}