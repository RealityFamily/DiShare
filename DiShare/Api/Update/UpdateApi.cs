﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Update.UpdateApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Abstract;
using DiShare.Api.Update.Responses;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Update
{
  public class UpdateApi : ApiBase, IUpdateApi
  {
    public UpdateApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<UpdateResponse> GetUpdateInfoAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/update").AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<UpdateResponse>(cancellationToken);
    }
  }
}
