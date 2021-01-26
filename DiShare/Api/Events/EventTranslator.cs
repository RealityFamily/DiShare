
using DiShare.Analytics.Models;
using DiShare.Api.Events.Requests;

namespace DiShare.Api.Events
{
  public static class EventTranslator
  {
    public static EventRequest ToRequest(this Event item, string hardwareId) => new EventRequest(item.Id, item.Action, item.Label, item.Created, item.Version, hardwareId, 1);
  }
}
