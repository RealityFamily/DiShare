
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
