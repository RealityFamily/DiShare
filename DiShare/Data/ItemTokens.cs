

using DiShare.Domain.Models;
using System.Collections.Generic;

namespace DiShare.Data
{
  public class ItemTokens
  {
    public const string ModelFolderName = "Model";
    public const string MaterialFolderName = "MaxMaterial";
    public const string AssetFolderName = "Asset";
    public static readonly string[] AssetExtensions = new string[19]
    {
      "tga",
      "png",
      "jpg",
      "jpeg",
      "tiff",
      "hdr",
      "exr",
      "gif",
      "bmp",
      "psd",
      "ms",
      "mse",
      "mzp",
      "mcr",
      "fbx",
      "3ds",
      "obj",
      "dwg",
      "blend"
    };
    public static readonly IReadOnlyDictionary<ItemType, string> ItemTypes = (IReadOnlyDictionary<ItemType, string>) new Dictionary<ItemType, string>()
    {
      {
        ItemType.Model,
        "Model"
      },
      {
        ItemType.Material,
        "MaxMaterial"
      },
      {
        ItemType.Asset,
        "Asset"
      }
    };
  }
}
