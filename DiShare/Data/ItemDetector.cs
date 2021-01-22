// Decompiled with JetBrains decompiler
// Type: DiShare.Data.ItemDetector
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Domain.Models;
using System.IO;

namespace DiShare.Data
{
  public class ItemDetector : IItemDetector
  {
    public bool IsItemFolder(string path)
    {
      try
      {
        return this.GetItemType(path) != ItemType.Unknown;
      }
      catch
      {
        return false;
      }
    }

    public ItemType GetItemType(string path)
    {
      foreach (ItemType key in ItemTokens.ItemTypes.Keys)
      {
        if (Directory.Exists(Path.Combine(path, ItemTokens.ItemTypes[key])))
          return key;
      }
      return ItemType.Unknown;
    }
  }
}
