// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Repository.ItemsRepository
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using Dapper;
using DiShare.Data.Database;
using DiShare.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace DiShare.Data.Repository
{
  public class ItemsRepository : IItemsRepository
  {
    private readonly IDatabase _database;

    public ItemsRepository(IDatabase database) => this._database = database;

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      if (!this._database.IsDataBaseInitialized)
        return (IEnumerable<Item>) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return await con.QueryAsync<Item>("SELECT Id, Name, Url, VendorId, ItemType, CategoryId, Description, ForRegistered, Price, Hash, Version, State, Size FROM Items ORDER By Name;").ConfigureAwait(false);
      }
    }

    public async Task<IEnumerable<Item>> GetItemsByCategoryIdAsync(int categoryId)
    {
      if (!this._database.IsDataBaseInitialized)
        return (IEnumerable<Item>) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return await con.QueryAsync<Item>("SELECT Id, Name, Url, VendorId, ItemType, CategoryId, Description, ForRegistered, Price, Hash, Version, State, Size FROM Items WHERE CategoryId = @categoryId ORDER By Name;", (object) new
        {
          categoryId = categoryId
        }).ConfigureAwait(false);
      }
    }

    public async Task<Item> GetItemsByIdAsync(string id)
    {
      if (!this._database.IsDataBaseInitialized)
        return (Item) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return (await con.QueryAsync<Item>("SELECT Id, Name, Url, VendorId, ItemType, CategoryId, Description, ForRegistered, Price, Hash, Version, State, Size FROM Items WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false)).FirstOrDefault<Item>();
      }
    }

    public async Task AddItemAsync(Item item)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("INSERT INTO Items (Id, Name, Url, VendorId, ItemType, CategoryId, Description, ForRegistered, Price, Hash, Version, State, Size) VALUES (@Id, @Name, @Url, @VendorId, @ItemType, @CategoryId, @Description, @ForRegistered, @Price, @Hash, @Version, @State, @Size);", (object) item).ConfigureAwait(false);
      }
    }

    public async Task UpdateItemAsync(string id, Item item)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("UPDATE Items SET Name = @Name, Url = @Url, VendorId = @VendorId, ItemType = @ItemType, CategoryId = @CategoryId, Description = @Description, ForRegistered = @ForRegistered, Version = @Version, Price = @Price, Hash = @Hash, State = @State, Size = @Size WHERE Id = @Id;", (object) new
        {
          Name = item.Name,
          Url = item.Url,
          VendorId = item.VendorId,
          ItemType = item.ItemType,
          CategoryId = item.CategoryId,
          Description = item.Description,
          ForRegistered = item.ForRegistered,
          Version = item.Version,
          Price = item.Price,
          Hash = item.Hash,
          State = item.State,
          Size = item.Size,
          Id = id
        }).ConfigureAwait(false);
      }
    }

    public async Task DeleteItemAsync(string id)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("DELETE FROM Items WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false);
      }
    }
  }
}
