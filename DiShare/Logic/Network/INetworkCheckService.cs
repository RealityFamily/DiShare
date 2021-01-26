

using DiShare.Logic.Network.Events;
using System;

namespace DiShare.Logic.Network
{
  public interface INetworkCheckService
  {
    event EventHandler<NetworkStateChangedEvent> OnNetworkStateChanged;

    bool IsConnected { get; }

    void BeginBackgroundChecking();
  }
}
