

using DiShare.Api.Base;
using DiShare.Api.Events.Requests;
using DiShare.Api.Events.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Events
{
  public interface IEventsApi
  {
    Task<Response<IEnumerable<EventResponse>>> PostEventsAsync(
      IEnumerable<EventRequest> events,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
