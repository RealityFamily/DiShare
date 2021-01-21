using DiShare.Infrastructure;
using Microsoft.Win32;

namespace DiShare.OS
{
    public interface IRegistryProvider
    {
        TryGetResult<string> TryGetValue(string keyName, string valueName);

        TryGetResult<T> TryGetValue<T>(string keyName, string valueName);

        TryGetResult<T> TryGetValue<T>(
            RegistryHive hKey,
            string keyName,
            string valueName,
            RegistryView view = RegistryView.Default);

        TryResult TrySetValue<T>(string key, string valueName, T value);

        TryResult TrySetValue<T>(
            string key,
            string valueName,
            T value,
            RegistryValueKind valueKind);

        TryResult TryDeleteValueHKCU(string subKey, string valueName, bool throwOnMissing);
    }
}
