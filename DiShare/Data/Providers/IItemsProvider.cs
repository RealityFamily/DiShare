// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Providers.IItemsProvider
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Domain.Models;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data.Providers
{
  public interface IItemsProvider
  {
    Task InitCacheAsync(string token = null);

    Task<TryResult<IReadOnlyCollection<ModelItem>>> GetItemsAsync(
      string categoryId);

    Task<TryResult<ModelItem>> GetItemByIdAsync(string id);

    Task<TryResult<IReadOnlyCollection<ModelItem>>> GetItemsAsync();

    Task<TryResult<ModelItem>> GetImagesAsync(ModelItem item);

    Task<TryResult> UpdateItemAsync(ModelItem item);

    Task<TryResult> ClearCacheItem(ModelItem item);
  }
}
