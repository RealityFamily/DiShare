

using DiShare.Infrastructure;
using DiShare.OS;

namespace DiShare.Logic.Windows8And10Detector
{
  public class Windows8And10Detector : IWindows8And10Detector
  {
    private readonly IRegistryProvider _registryProvider;
    private readonly string CurrentVersionHKLMKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
    private readonly string ProductNameValueName = "ProductName";
    private readonly string Windows10Text = "Windows 10";
    private readonly string Windows8Text = "Windows 8";

    public Windows8And10Detector(IRegistryProvider registryProvider) => this._registryProvider = registryProvider;

    public TryResult<bool> IsWindows8Or10()
    {
      TryGetResult<string> tryGetResult = this._registryProvider.TryGetValue<string>(this.CurrentVersionHKLMKey, this.ProductNameValueName);
      return new TryResult<bool>(tryGetResult.IsFound && (tryGetResult.Value.StartsWith(this.Windows8Text) || tryGetResult.Value.StartsWith(this.Windows10Text)));
    }
  }
}
