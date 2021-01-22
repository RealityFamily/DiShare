// Decompiled with JetBrains decompiler
// Type: Logic.TempFolderCleaner.TempFolderCleaner
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
