// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Events.Requests.EventRequest
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using System;
using System.Runtime.Serialization;

namespace DiShare.Api.Events.Requests
{
  [DataContract]
  public class EventRequest
  {
    [DataMember]
    public string Id { get; }

    [DataMember]
    public string EventName { get; }

    [DataMember]
    public string ExtraData { get; }

    [DataMember]
    public string ClientVersion { get; }

    [DataMember]
    public string HardwareId { get; }

    [DataMember]
    public DateTime CreatedAt { get; }

    [DataMember]
    public int Count { get; }

    public EventRequest(
      string id,
      string action,
      string label,
      DateTime createdAt,
      string clientVersion,
      string hardwareId,
      int count)
    {
      this.Id = id;
      this.EventName = action;
      this.ExtraData = label;
      this.CreatedAt = createdAt;
      this.ClientVersion = clientVersion;
      this.HardwareId = hardwareId;
      this.Count = count;
    }
  }
}
