

using DiShare.Infrastructure;
using System.Threading.Tasks;

namespace DiShare.Data.CacheFolderProvider
{
  public interface ICacheFolderProvider
  {
    Task<TryResult<string>> GetFolderAsync();

    Task SetFolder(string path);
  }
}
