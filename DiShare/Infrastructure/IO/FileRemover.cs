

using System;
using System.IO;
using System.Threading.Tasks;

namespace DiShare.Infrastructure.IO
{
  public class FileRemover : IFileRemover
  {
    public async void RemoveFile(string filename)
    {
      FileInfo file = new FileInfo(filename);
      while (this.IsFileLocked(file))
        await Task.Delay(500).ConfigureAwait(false);
      try
      {
        file.Delete();
      }
      catch (Exception ex)
      {
      }
    }

    protected virtual bool IsFileLocked(FileInfo file)
    {
      FileStream fileStream = (FileStream) null;
      try
      {
        fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
      }
      catch (IOException ex)
      {
        return true;
      }
      finally
      {
        fileStream?.Close();
      }
      return false;
    }
  }
}
