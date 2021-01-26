

using DiShare.Logic.Updater.Models;
using System.Threading.Tasks;

namespace DiShare.Logic.Updater.Providers
{
  public interface IUpdateInfoProvider
  {
    Task<UpdateInfo> GetUpdateInfoAsync();
  }
}
