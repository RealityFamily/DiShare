

using Dapper;
using DiShare.Infrastructure.Extensions;
using System;
using System.Data.SQLite;
using System.IO;

namespace DiShare.Data.Database
{
  public class DatabaseProvider : IDatabase
  {
    private readonly string _dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "3DHamster", "catalog.db");

    public bool IsDataBaseInitialized { get; private set; }

    public SQLiteConnection GetConnection() => new SQLiteConnection("Data Source=" + this._dbFile);

    public DatabaseProvider()
    {
      if (!File.Exists(this._dbFile))
        this.CreateDatabase();
      else
        this.IsDataBaseInitialized = true;
    }

    private void CreateDatabase()
    {
      try
      {
        Path.GetDirectoryName(this._dbFile).CreateDirectoryIfMissing();
        string sql = "CREATE TABLE Categories\r\n                    (\r\n                        Id INT PRIMARY KEY,\r\n                        Name VARCHAR(255) NOT NULL, \r\n                        CreatedAt DATETIME NOT NULL,\r\n                        CategoryId INT\r\n                    );\r\n                    CREATE TABLE Vendors\r\n                    (\r\n                        Id INT PRIMARY KEY,\r\n                        Name VARCHAR(255) NOT NULL, \r\n                        Url VARCHAR(255)\r\n                    );\r\n                    CREATE TABLE Items\r\n                    (\r\n                        Id VARCHAR(38) PRIMARY KEY,\r\n                        Name VARCHAR(255) NOT NULL, \r\n                        Url VARCHAR(255),\r\n                        VendorId INT NOT NULL,\r\n                        ItemType VARCHAR(16) NOT NULL,\r\n                        CategoryId INT NOT NULL,\r\n                        Description TEXT, \r\n                        ForRegistered BOOLEAN NOT NULL,\r\n                        Price DECIMAL(18,2),\r\n                        Hash VARCHAR(64) NOT NULL,\r\n                        Version INT NOT NULL,\r\n                        State INT NOT NULL,\r\n                        Size INT NOT NULL\r\n                    );";
        using (SQLiteConnection connection = this.GetConnection())
        {
          connection.Open();
          connection.Execute(sql);
        }
        this.IsDataBaseInitialized = true;
      }
      catch
      {
        this.IsDataBaseInitialized = false;
      }
    }
  }
}
