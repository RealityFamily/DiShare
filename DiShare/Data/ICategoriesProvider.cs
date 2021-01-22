// Decompiled with JetBrains decompiler
// Type: DiShare.Data.ICategoriesProvider
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Domain.Models;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data
{
  public interface ICategoriesProvider
  {
    Task InitCacheAsync();

    Task<TryResult<ICollection<CategoryItem>>> GetItemsAsync();

    bool HasBase(string baseName);

    void RemoveBase(string baseName);

    IReadOnlyCollection<string> GetBases();

    Task<TryResult<Manifest>> GetBaseManifestAsync(string baseName);

    bool HasStarterBase();

    bool HasExtendedBase();

    string BasePath { get; set; }
  }
}
