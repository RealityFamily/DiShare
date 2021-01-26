

using DiShare.Api.Items.Responses;
using DiShare.Domain.DTO;
using DiShare.Domain.Models;
using System;

namespace DiShare.Services.Extensions
{
  public static class ItemExtension
  {
    public static ItemDto ToDto(this ItemResponse item)
    {
      ItemType result;
      if (!Enum.TryParse<ItemType>(item.ItemType, out result))
        result = ItemType.Unknown;
      return new ItemDto()
      {
        Id = item.Id,
        Name = item.Name,
        Url = item.Url,
        VendorId = item.VendorId,
        ItemTypeId = item.ItemTypeId,
        ItemType = result,
        CategoryId = item.CategoryId,
        Description = item.Description,
        ForRegistered = item.ForRegistered,
        CreatedAt = item.CreatedAt,
        Version = item.Version,
        Hash = item.Hash,
        Price = item.Price,
        Size = item.Size
      };
    }
  }
}
