// Decompiled with JetBrains decompiler
// Type: Logic.Network.INetworkCheckService
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
