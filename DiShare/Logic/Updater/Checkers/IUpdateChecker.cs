

using DiShare.Infrastructure;
using DiShare.Logic.Updater.Models;
using System.Threading.Tasks;

namespace DiShare.Logic.Updater.Checkers
{
  public interface IUpdateChecker
  {
    Task<TryResult<UpdateInfo>> GetUpdateInfoAsync();
  }
}
