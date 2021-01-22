// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Events.EventsApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Events.Requests;
using DiShare.Api.Events.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Events
{
  public class EventsApi : ApiBase, IEventsApi
  {
    public EventsApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<IEnumerable<EventResponse>>> PostEventsAsync(
      IEnumerable<EventRequest> events,
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.PostRequest("api/v1/events").AddContent<IEnumerable<EventRequest>>(events).AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<IEnumerable<EventResponse>>>(cancellationToken);
    }
  }
}
