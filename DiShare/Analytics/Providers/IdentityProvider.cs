// Decompiled with JetBrains decompiler
// Type: DiShare.Analytics.Providers.IdentityProvider
// Assembly: DiShare.Analytics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 074377AB-5F2B-4ED6-AC1C-A43B51B8190A
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Analytics.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace DiShare.Analytics.Providers
{
  public class IdentityProvider : IIdentityProvider
  {
    private readonly string HwId;

    public IdentityProvider()
    {
      using (MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider())
        this.HwId = this.GetHexString(((IEnumerable<byte>) cryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(this.GetId()))).ToArray<byte>());
    }

    public string GetHardwareId() => this.HwId;

    private string GetHexString(byte[] hash)
    {
      string str1 = string.Empty;
      for (int index = 0; index < hash.Length; ++index)
      {
        int num1 = (int) hash[index];
        int num2 = (int) hash[index] & 15;
        int num3 = (int) hash[index] >> 4 & 15;
        char ch;
        string str2;
        if (num3 > 9)
        {
          string str3 = str1;
          ch = (char) (num3 - 10 + 65);
          string str4 = ch.ToString();
          str2 = str3 + str4;
        }
        else
          str2 = str1 + num3.ToString();
        if (num2 > 9)
        {
          string str3 = str2;
          ch = (char) (num2 - 10 + 65);
          string str4 = ch.ToString();
          str1 = str3 + str4;
        }
        else
          str1 = str2 + num2.ToString();
        if (index + 1 != hash.Length && (index + 1) % 4 == 0)
          str1 += "-";
      }
      return str1;
    }

    private string GetId()
    {
      string str1;
      using (IdentityProvider.HardwareParams hardwareParams = new IdentityProvider.HardwareParams("Win32_BIOS"))
        str1 = hardwareParams.GetParameter("Manufacturer") + hardwareParams.GetParameter("SMBIOSBIOSVersion") + hardwareParams.GetParameter("IdentificationCode") + hardwareParams.GetParameter("SerialNumber") + hardwareParams.GetParameter("ReleaseDate") + hardwareParams.GetParameter("Version");
      string str2;
      using (IdentityProvider.HardwareParams hardwareParams = new IdentityProvider.HardwareParams("Win32_BaseBoard"))
        str2 = hardwareParams.GetParameter("Model") + hardwareParams.GetParameter("Manufacturer") + hardwareParams.GetParameter("Product") + hardwareParams.GetParameter("SerialNumber");
      string str3;
      using (IdentityProvider.HardwareParams hardwareParams = new IdentityProvider.HardwareParams("Win32_Processor"))
        str3 = hardwareParams.GetParameter("UniqueId") + hardwareParams.GetParameter("ProcessorId") + hardwareParams.GetParameter("Name") + hardwareParams.GetParameter("Manufacturer");
      return str1 + str2 + str3;
    }

    private class HardwareParams : IDisposable
    {
      private readonly ManagementObjectCollection instances;
      private ManagementClass managementProperties;

      public HardwareParams(string managementSection)
      {
        this.managementProperties = new ManagementClass(managementSection);
        this.instances = this.managementProperties.GetInstances();
      }

      public void Dispose()
      {
        this.managementProperties.Dispose();
        this.managementProperties = (ManagementClass) null;
      }

      public string GetParameter(string paramName)
      {
        foreach (ManagementBaseObject instance in this.instances)
        {
          object propertyValue = instance.GetPropertyValue(paramName);
          if (propertyValue != null)
            return propertyValue?.ToString() ?? "";
        }
        return (string) null;
      }
    }
  }
}
