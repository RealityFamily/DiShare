

using Dapper;
using DiShare.Data.Database;
using DiShare.Domain.Entities;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace DiShare.Data.Repository
{
  public class CategoriesRepository : ICategoriesRepository
  {
    private readonly IDatabase _database;

    public CategoriesRepository(IDatabase database) => this._database = database;

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      if (!this._database.IsDataBaseInitialized)
        return (IEnumerable<Category>) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return await con.QueryAsync<Category>("SELECT Id, Name, CategoryId, CreatedAt FROM Categories ORDER By Name;").ConfigureAwait(false);
      }
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
      if (!this._database.IsDataBaseInitialized)
        return (Category) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return (await con.QueryAsync<Category>("SELECT Id, Name, CategoryId, CreatedAt FROM Categories WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false)).FirstOrDefault<Category>();
      }
    }

    public async Task AddCategoryAsync(Category category)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("INSERT INTO Categories (Id, Name, CategoryId, CreatedAt) VALUES (@Id, @Name, @CategoryId, @CreatedAt);", (object) category).ConfigureAwait(false);
      }
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("UPDATE Categories SET Id = @Id, Name = @Name, CategoryId = @CategoryId, CreatedAt = @CreatedAt WHERE Id = @Id;", (object) new
        {
          Name = category.Name,
          CategoryId = category.CategoryId,
          CreatedAt = category.CreatedAt,
          Id = id
        }).ConfigureAwait(false);
      }
    }

    public async Task DeleteCategoryAsync(int id)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("DELETE FROM Categories WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false);
      }
    }
  }
}
