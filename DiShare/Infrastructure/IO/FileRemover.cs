// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.IO.FileRemover
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

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
