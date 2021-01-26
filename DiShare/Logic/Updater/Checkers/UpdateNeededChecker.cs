

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
