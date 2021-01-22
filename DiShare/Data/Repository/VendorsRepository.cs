// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Repository.VendorsRepository
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
  public class VendorsRepository : IVendorsRepository
  {
    private readonly IDatabase _database;

    public VendorsRepository(IDatabase database) => this._database = database;

    public async Task<IEnumerable<Vendor>> GetVendorsAsync()
    {
      if (!this._database.IsDataBaseInitialized)
        return (IEnumerable<Vendor>) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return await con.QueryAsync<Vendor>("SELECT Id, Name, Url FROM Vendors ORDER By Name;").ConfigureAwait(false);
      }
    }

    public async Task<Vendor> GetVendorByIdAsync(int id)
    {
      if (!this._database.IsDataBaseInitialized)
        return (Vendor) null;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        return (await con.QueryAsync<Vendor>("SELECT Id, Name, Url FROM Vendors WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false)).FirstOrDefault<Vendor>();
      }
    }

    public async Task AddVendorAsync(Vendor vendor)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("INSERT INTO Vendors (Id, Name, Url) VALUES (@Id, @Name, @Url);", (object) vendor).ConfigureAwait(false);
      }
    }

    public async Task UpdateVendorAsync(int id, Vendor vendor)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("UPDATE Vendors SET Name = @Name, Url = @Url WHERE Id = @Id;", (object) vendor).ConfigureAwait(false);
      }
    }

    public async Task DeleteVendorAsync(int id)
    {
      if (!this._database.IsDataBaseInitialized)
        return;
      using (SQLiteConnection con = this._database.GetConnection())
      {
        await con.OpenAsync().ConfigureAwait(false);
        int num = await con.ExecuteAsync("DELETE FROM Vendors WHERE Id = @id;", (object) new
        {
          id = id
        }).ConfigureAwait(false);
      }
    }
  }
}
