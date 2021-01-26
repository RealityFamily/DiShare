

using System;

namespace DiShare.Domain.Entities
{
  public class Item
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public int VendorId { get; set; }

    public string ItemType { get; set; }

    public int CategoryId { get; set; }

    public string Description { get; set; }

    public bool ForRegistered { get; set; }

    public int Version { get; set; }

    public int State { get; set; }

    public Decimal? Price { get; set; }

    public string Hash { get; set; }

    public long Size { get; set; }
  }
}
