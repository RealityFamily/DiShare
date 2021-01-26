

using System.Data.SQLite;

namespace DiShare.Data.Database
{
  public interface IDatabase
  {
    bool IsDataBaseInitialized { get; }

    SQLiteConnection GetConnection();
  }
}
