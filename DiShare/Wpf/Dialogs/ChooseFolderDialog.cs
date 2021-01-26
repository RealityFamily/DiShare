
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
