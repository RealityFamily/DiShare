// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.Extensions.FileExtensions
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System.IO;

namespace Library.Infrastructure.Extensions
{
  public static class FileExtensions
  {
    public static void CreateDirectoryIfMissing(this string path)
    {
      if (Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }

    public static void RemoveDirectory(this string path, bool recursive = true)
    {
      if (path.IsNullOrEmpty() || !Directory.Exists(path))
        return;
      Directory.Delete(path, recursive);
    }

    public static void RemoveFile(this string path)
    {
      if (path.IsNullOrEmpty() || !File.Exists(path))
        return;
      File.Delete(path);
    }

    public static void RemoveFileSafe(this string path)
    {
      try
      {
        path.RemoveFile();
      }
      catch
      {
      }
    }

    public static void RemoveDirectorySafe(this string path)
    {
      try
      {
        path.RemoveDirectory();
      }
      catch
      {
      }
    }

    public static void RemoveEmptyDirectorySafe(this string path)
    {
      try
      {
        if (Directory.GetDirectories(path).Length != 0 || Directory.GetFiles(path).Length != 0)
          return;
        path.RemoveDirectory(false);
      }
      catch
      {
      }
    }
  }
}
