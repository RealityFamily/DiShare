
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
