

using DiShare.Infrastructure.Extensions;
using System.IO;

namespace DiShare.Logic.TempFolderCleaner
{
  public class TempFolderCleaner : ITempFolderCleaner
  {
    public void Execute()
    {
      foreach (string file in Directory.GetFiles(Path.GetTempPath(), "LibraryModelScript_*.ms"))
        file.RemoveFileSafe();
    }
  }
}
