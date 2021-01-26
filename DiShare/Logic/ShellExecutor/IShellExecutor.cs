

namespace DiShare.Logic.ShellExecutor
{
  public interface IShellExecutor
  {
    void Execute(string uri);

    void Execute(string filename, string arguments);
  }
}
