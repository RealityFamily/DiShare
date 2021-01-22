// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Models.Responses.ModelResponse
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using Newtonsoft.Json;

namespace DiShare.Api.Models.Responses
{
  public class ModelResponse
  {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("updated")]
    public string Updated { get; set; }

    [JsonProperty("hashsum")]
    public string HashSum { get; set; }
  }
}
