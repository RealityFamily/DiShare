// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Extensions.ItemExtensions
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Data.Exceptions;
using DiShare.Domain.DTO;
using DiShare.Domain.Entities;
using DiShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DiShare.Data.Extensions
{
  public static class ItemExtensions
  {
    public static string GetAssetFileName(this ModelItem item)
    {
      if (item.Type != ItemType.Asset)
        throw new RepositoryException("BaseItem is not asset");
      return ((IEnumerable<string>) Directory.GetFiles(Path.Combine(item.Path, "Asset"), "*.*")).FirstOrDefault<string>((Func<string, bool>) (c => ((IEnumerable<string>) ItemTokens.AssetExtensions).Contains<string>(Path.GetExtension(c).Replace(".", ""))));
    }

    public static Item ToEntity(this ItemDto item) => new Item()
    {
      Id = item.Id,
      CategoryId = item.CategoryId,
      Name = item.Name,
      ForRegistered = item.ForRegistered,
      VendorId = item.VendorId,
      Version = item.Version,
      Description = item.Description,
      ItemType = item.ItemType.ToString(),
      Url = item.Url,
      State = 1,
      Hash = item.Hash,
      Price = item.Price,
      Size = item.Size
    };

    public static ModelItem ToModel(
      this Item item,
      IEnumerable<VendorDto> vendors,
      string cacheFolderPath)
    {
      ItemType result;
      if (!Enum.TryParse<ItemType>(item.ItemType, out result))
        result = ItemType.Unknown;
      ModelItem modelItem = new ModelItem();
      modelItem.Id = item.Id.ToString();
      modelItem.Type = result;
      modelItem.Name = item.Name;
      modelItem.ForRegistered = item.ForRegistered;
      modelItem.Vendor = vendors.FirstOrDefault<VendorDto>((Func<VendorDto, bool>) (v => v.Id == item.VendorId))?.Name;
      modelItem.Description = item.Description;
      modelItem.Url = item.Url;
      modelItem.State = (ItemState) item.State;
      modelItem.Hash = item.Hash;
      modelItem.Price = item.Price;
      modelItem.Size = item.Size;
      modelItem.Path = Path.Combine(cacheFolderPath, "Cache", item.Id);
      return modelItem;
    }

    public static bool IsDownloadingState(this ModelItem item) => item.State == ItemState.Downloading || item.State == ItemState.Extraction || item.State == ItemState.Queued;
  }
}
