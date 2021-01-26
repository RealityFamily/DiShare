

using DiShare.Infrastructure;

namespace DiShare.Logic.Windows8And10Detector
{
  public interface IWindows8And10Detector
  {
    TryResult<bool> IsWindows8Or10();
  }
}
