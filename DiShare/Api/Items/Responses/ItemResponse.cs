

using Newtonsoft.Json;
using System;

namespace DiShare.Api.Items.Responses
{
  public class ItemResponse
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("vendorId")]
    public int VendorId { get; set; }

    [JsonProperty("vendor")]
    public string Vendor { get; set; }

    [JsonProperty("vendorUrl")]
    public string VendorUrl { get; set; }

    [JsonProperty("itemTypeId")]
    public int ItemTypeId { get; set; }

    [JsonProperty("itemType")]
    public string ItemType { get; set; }

    [JsonProperty("categoryId")]
    public int CategoryId { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("forRegistered")]
    public bool ForRegistered { get; set; }

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("version")]
    public int Version { get; set; }

    [JsonProperty("hash")]
    public string Hash { get; set; }

    [JsonProperty("price")]
    public Decimal? Price { get; set; }

    [JsonProperty("size")]
    public long Size { get; set; }
  }
}
