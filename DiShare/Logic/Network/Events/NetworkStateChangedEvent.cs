

using System;

namespace DiShare.Logic.Network.Events
{
  public class NetworkStateChangedEvent : EventArgs
  {
    public bool IsConnected { get; }

    public NetworkStateChangedEvent(bool isConnected) => this.IsConnected = isConnected;
  }
}
