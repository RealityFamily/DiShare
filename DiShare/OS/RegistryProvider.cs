
using DiShare.Infrastructure;
using Microsoft.Win32;
using System;

namespace DiShare.OS.Registry
{
  public class RegistryProvider : IRegistryProvider
  {
    private readonly string HKCUName = "HKEY_CURRENT_USER\\";

    public TryGetResult<string> TryGetValue(string keyName, string valueName) => this.TryGetValue<string>(keyName, valueName);

    public TryGetResult<T> TryGetValue<T>(
      RegistryHive hKey,
      string keyName,
      string valueName,
      RegistryView view = RegistryView.Default)
    {
      RegistryKey registryKey = RegistryKey.OpenBaseKey(hKey, view).OpenSubKey(keyName);
      if (registryKey == null)
        return TryGetResult.NotFound<T>();
      T result = (T) registryKey.GetValue(valueName, (object) null);
      registryKey.Close();
      return (object) result != null ? TryGetResult.Found<T>(result) : TryGetResult.NotFound<T>();
    }

    public TryGetResult<T> TryGetValue<T>(string keyName, string valueName)
    {
      try
      {
        T result = (T) Microsoft.Win32.Registry.GetValue(keyName, valueName, (object) null);
        return (object) result == null ? TryGetResult.NotFound<T>() : TryGetResult.Found<T>(result);
      }
      catch
      {
        return TryGetResult.NotFound<T>();
      }
    }

    public TryResult TrySetValue<T>(string key, string valueName, T value) => this.TrySetValue<T>(key, valueName, value, RegistryValueKind.String);

    public TryResult TrySetValue<T>(
      string key,
      string valueName,
      T value,
      RegistryValueKind valueKind)
    {
      try
      {
        Microsoft.Win32.Registry.SetValue(key, valueName, (object) value, valueKind);
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
      return new TryResult();
    }

    public TryResult TryDeleteValueHKCU(
      string subKey,
      string valueName,
      bool throwOnMissing)
    {
      try
      {
        if (subKey.StartsWith(this.HKCUName))
          subKey = subKey.Remove(0, this.HKCUName.Length);
        using (RegistryKey registryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey, true))
          registryKey?.DeleteValue(valueName, throwOnMissing);
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
      return new TryResult();
    }
  }
}
