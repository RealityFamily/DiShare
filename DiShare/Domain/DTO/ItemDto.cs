
using DiShare.Domain.Entities;
using DiShare.Domain.Models;
using DiShare.Infrastructure.Extensions;
using System;

namespace DiShare.Domain.DTO
{
  public class ItemDto
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public string Url { get; set; }

    public int VendorId { get; set; }

    public string Vendor { get; set; }

    public string VendorUrl { get; set; }

    public int ItemTypeId { get; set; }

    public ItemType ItemType { get; set; }

    public int CategoryId { get; set; }

    public string Description { get; set; }

    public bool ForRegistered { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Decimal? Price { get; set; }

    public int Version { get; set; }

    public string Hash { get; set; }

    public long Size { get; set; }

    public override bool Equals(object obj)
    {
      if (!(obj is Item obj1))
        return base.Equals(obj);
      if (obj1.Id.IsEqualsIgnoreCase(this.Id) && obj1.Name.IsEqualsIgnoreCase(this.Name) && obj1.VendorId == this.VendorId && (obj1.ItemType.IsEqualsIgnoreCase(this.ItemType.ToString()) && obj1.Description.IsEqualsIgnoreCase(this.Description) && (obj1.ForRegistered == this.ForRegistered && obj1.CategoryId == this.CategoryId)))
      {
        if (obj1.Price.HasValue || this.Price.HasValue)
        {
          Decimal? price1 = obj1.Price;
          Decimal? price2 = this.Price;
          if (!(price1.GetValueOrDefault() == price2.GetValueOrDefault() & price1.HasValue == price2.HasValue))
            goto label_6;
        }
        if (obj1.Version == this.Version && obj1.Hash.IsEqualsIgnoreCase(this.Hash))
          return obj1.Size == this.Size;
      }
label_6:
      return false;
    }
  }
}
