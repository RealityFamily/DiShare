// Decompiled with JetBrains decompiler
// Type: Logic.MaxVersionChecker.MaxVersionChecker
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using System.Text.RegularExpressions;

namespace DiShare.Logic.MaxVersionChecker
{
  public class MaxVersionChecker : IMaxVersionChecker
  {
    private const string VersionCheckPattern = "^(201[0-3] - (32|64)bit)|((201[4-9]|202[0-9]) - 64bit)$";

    public bool IsSupportedVersion(string version) => Regex.Match(version, "^(201[0-3] - (32|64)bit)|((201[4-9]|202[0-9]) - 64bit)$").Success;
  }
}
