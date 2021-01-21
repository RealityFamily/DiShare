// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Checkers.UpdateNeededChecker
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Logic.Updater.Models;
using System;

namespace DiShare.Logic.Updater.Checkers
{
  public class UpdateNeededChecker : IUpdateNeededChecker
  {
    public bool CheckIsUpdateNeeded(VersionInfo current, UpdateInfo updateInfo)
    {
      if (current == (VersionInfo) null)
        throw new ArgumentNullException(nameof (current));
      if (updateInfo == null)
        return false;
      return updateInfo.LastCriticalVersion > current || updateInfo.Version > current;
    }
  }
}
