

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
