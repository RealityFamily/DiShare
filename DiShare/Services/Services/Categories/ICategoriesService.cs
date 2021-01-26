

using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Categories
{
  public interface ICategoriesService
  {
    Task<TryResult<IReadOnlyCollection<CategoryDto>>> GetAsync(
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
