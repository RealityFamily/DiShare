

using DiShare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data.Repository
{
  public interface IItemsRepository
  {
    Task<IEnumerable<Item>> GetItemsAsync();

    Task<IEnumerable<Item>> GetItemsByCategoryIdAsync(int categoryId);

    Task<Item> GetItemsByIdAsync(string id);

    Task AddItemAsync(Item item);

    Task UpdateItemAsync(string id, Item item);

    Task DeleteItemAsync(string id);
  }
}
