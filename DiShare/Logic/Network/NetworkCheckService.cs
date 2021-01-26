

using DiShare.Common;
using DiShare.Infrastructure.Network;
using DiShare.Logic.Network.Events;
using System;
using System.Threading.Tasks;

namespace DiShare.Logic.Network
{
  public class NetworkCheckService : INetworkCheckService
  {
    private readonly INetworkChecker _networkChecker;

    public NetworkCheckService(INetworkChecker networkChecker) => this._networkChecker = networkChecker;

    public event EventHandler<NetworkStateChangedEvent> OnNetworkStateChanged;

    public bool IsConnected { get; private set; }

    public void BeginBackgroundChecking() => Task.Run(new Action(this.StartAsync));

    private async void StartAsync()
    {
      NetworkCheckService networkCheckService = this;
      while (true)
      {
        bool isConnected = networkCheckService._networkChecker.CheckIsConnected();
        // ISSUE: explicit non-virtual call
        if (isConnected !=  (networkCheckService.IsConnected))
        {
          networkCheckService.IsConnected = isConnected;
          EventHandler<NetworkStateChangedEvent> networkStateChanged = networkCheckService.OnNetworkStateChanged;
          if (networkStateChanged != null)
            networkStateChanged((object) networkCheckService, new NetworkStateChangedEvent(isConnected));
        }
        await Task.Delay(ConfigurationTokens.ConnectionCheckIntervalInSeconds).ConfigureAwait(false);
      }
    }
  }
}
