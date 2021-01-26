

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
