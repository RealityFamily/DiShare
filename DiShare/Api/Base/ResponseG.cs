
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
