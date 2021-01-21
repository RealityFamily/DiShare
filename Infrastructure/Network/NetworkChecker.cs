// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.Network.NetworkChecker
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace Library.Infrastructure.Network
{
  public class NetworkChecker : INetworkChecker
  {
    public bool CheckIsConnected()
    {
      try
      {
        if (((IEnumerable<NetworkInterface>) NetworkInterface.GetAllNetworkInterfaces()).Any<NetworkInterface>((Func<NetworkInterface, bool>) (nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType != NetworkInterfaceType.Tunnel && nic.OperationalStatus == OperationalStatus.Up)))
        {
          using (WebClient webClient = new WebClient())
          {
            using (webClient.OpenRead("http://google.com/generate_204"))
              return true;
          }
        }
      }
      catch
      {
      }
      return false;
    }
  }
}
