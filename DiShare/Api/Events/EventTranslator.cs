// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Events.EventTranslator
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Analytics.Models;
using DiShare.Api.Events.Requests;

namespace DiShare.Api.Events
{
  public static class EventTranslator
  {
    public static EventRequest ToRequest(this Event item, string hardwareId) => new EventRequest(item.Id, item.Action, item.Label, item.Created, item.Version, hardwareId, 1);
  }
}
