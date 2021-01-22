// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Dialogs.ChooseFolderDialog
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System.Windows.Forms;

namespace DiShare.Wpf.Dialogs
{
  public class ChooseFolderDialog : IChooseFolderDialog
  {
    public string ShowDialog(string title, string folder)
    {
      using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
      {
        string str = folder;
        folderBrowserDialog.SelectedPath = folder;
        folderBrowserDialog.Description = title;
        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
          str = folderBrowserDialog.SelectedPath;
        return str;
      }
    }
  }
}
