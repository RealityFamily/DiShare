// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Models.VersionInfo
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using System;
using System.Diagnostics.CodeAnalysis;

namespace DiShare.Logic.Updater.Models
{
  public class VersionInfo : IComparable<VersionInfo>, IEquatable<VersionInfo>
  {
    public int Major { get; }

    public int Minor { get; }

    public int Build { get; }

    public int Revision { get; }

    public VersionInfo(int major, int minor, int build, int revision)
    {
      this.Major = major;
      this.Minor = minor;
      this.Build = build;
      this.Revision = revision;
    }

    public static bool operator <(VersionInfo nfoLeft, VersionInfo nfoRight)
    {
      if (nfoLeft == (VersionInfo) null && nfoRight == (VersionInfo) null)
        return false;
      return nfoLeft == (VersionInfo) null || nfoLeft.CompareTo(nfoRight) < 0;
    }

    public static bool operator >(VersionInfo nfoLeft, VersionInfo nfoRight) => (!(nfoLeft == (VersionInfo) null) || !(nfoRight == (VersionInfo) null)) && !(nfoLeft == (VersionInfo) null) && nfoLeft.CompareTo(nfoRight) > 0;

    public static bool operator <=(VersionInfo nfoLeft, VersionInfo nfoRight) => nfoLeft == (VersionInfo) null && nfoRight == (VersionInfo) null || nfoLeft == (VersionInfo) null || nfoLeft.CompareTo(nfoRight) <= 0;

    public static bool operator >=(VersionInfo nfoLeft, VersionInfo nfoRight)
    {
      if (nfoLeft == (VersionInfo) null && nfoRight == (VersionInfo) null)
        return true;
      return !(nfoLeft == (VersionInfo) null) && nfoLeft.CompareTo(nfoRight) >= 0;
    }

    public static bool operator ==(VersionInfo nfoLeft, VersionInfo nfoRight) => (object) nfoLeft == null ? (object) nfoRight == null : (nfoLeft.Equals(nfoRight));

    public static bool operator !=(VersionInfo nfoLeft, VersionInfo nfoRight) => !(nfoLeft == nfoRight);

    public int CompareTo(VersionInfo other)
    {
      if (other == (VersionInfo) null)
        return 1;
      int num1 = this.Major;
      int num2 = num1.CompareTo(other.Major);
      if (num2 != 0)
        return num2;
      num1 = this.Minor;
      int num3 = num1.CompareTo(other.Minor);
      if (num3 != 0)
        return num3;
      num1 = this.Build;
      return num1.CompareTo(other.Build);
    }

    public override string ToString() => string.Format("{0}.{1}.{2}.{3}", (object) this.Major, (object) this.Minor, (object) this.Build, (object) this.Revision);

    public bool Equals(VersionInfo other)
    {
      if ((object) other == null)
        return false;
      if ((object) this == (object) other)
        return true;
      return this.Major == other.Major && this.Minor == other.Minor && this.Build == other.Build;
    }

    [ExcludeFromCodeCoverage]
    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if ((object) this == obj)
        return true;
      return !(obj.GetType() != this.GetType()) && this.Equals((VersionInfo) obj);
    }

    [ExcludeFromCodeCoverage]
    public override int GetHashCode() => ((this.Major * 397 ^ this.Minor) * 397 ^ this.Build) * 397 ^ this.Revision;
  }
}
