// Decompiled with JetBrains decompiler
// Type: DiShare.Data.ItemTokens
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

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
