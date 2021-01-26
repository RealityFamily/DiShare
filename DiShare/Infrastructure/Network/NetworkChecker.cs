﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace DiShare.Infrastructure.Network
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
