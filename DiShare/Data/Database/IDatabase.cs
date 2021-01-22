// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Database.IDatabase
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using System.Data.SQLite;

namespace DiShare.Data.Database
{
  public interface IDatabase
  {
    bool IsDataBaseInitialized { get; }

    SQLiteConnection GetConnection();
  }
}
