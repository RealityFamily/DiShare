

using DiShare.Domain.Models;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data
{
  public interface ICategoriesProvider
  {
    Task InitCacheAsync();

    Task<TryResult<ICollection<CategoryItem>>> GetItemsAsync();

    bool HasBase(string baseName);

    void RemoveBase(string baseName);

    IReadOnlyCollection<string> GetBases();

    Task<TryResult<Manifest>> GetBaseManifestAsync(string baseName);

    bool HasStarterBase();

    bool HasExtendedBase();

    string BasePath { get; set; }
  }
}
