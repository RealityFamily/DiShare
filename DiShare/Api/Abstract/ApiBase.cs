// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Abstract.ApiBase
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Common;
using Tiny.RestClient;

namespace DiShare.Api.Abstract
{
  public abstract class ApiBase
  {
    protected readonly string ApiKey = ConfigurationTokens.ApiKey;
    protected TinyRestClient Client;

    protected ApiBase(TinyRestClient client) => this.Client = client;
  }
}
