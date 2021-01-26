

using DiShare.Logic.Updater.Models;

namespace DiShare.Logic.Updater.Checkers
{
  public interface IUpdateNeededChecker
  {
    bool CheckIsUpdateNeeded(VersionInfo current, UpdateInfo updateInfo);
  }
}
