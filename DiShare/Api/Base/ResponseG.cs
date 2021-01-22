// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Base.Response`1
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using Newtonsoft.Json;

namespace DiShare.Api.Base
{
  public class Response<T>
  {
    [JsonProperty("status")]
    public ResponseStatus Status { get; set; }

    [JsonProperty("result")]
    public T Result { get; set; }

    [JsonProperty("error")]
    public string Error { get; set; }
  }
}
