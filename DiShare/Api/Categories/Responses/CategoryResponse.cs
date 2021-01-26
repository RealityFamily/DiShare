
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
