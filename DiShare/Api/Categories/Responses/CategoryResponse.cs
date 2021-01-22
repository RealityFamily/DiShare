// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Categories.Responses.CategoryResponse
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using Newtonsoft.Json;
using System;

namespace DiShare.Api.Categories.Responses
{
  public class CategoryResponse
  {
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("categoryId")]
    public int? CategoryId { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }
  }
}
