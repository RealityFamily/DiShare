

using DiShare.Logic.Updater.Models;

namespace DiShare.Logic.Updater.Builders
{
  public interface IVersionInfoBuilder
  {
    VersionInfo Build(string version);
  }
}
