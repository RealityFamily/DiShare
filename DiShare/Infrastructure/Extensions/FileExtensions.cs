using System.IO;

namespace DiShare.Infrastructure.Extensions
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
