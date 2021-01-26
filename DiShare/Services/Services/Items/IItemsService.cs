

using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Items
{
  public interface IItemsService
  {
    Task<TryResult<IReadOnlyCollection<ItemDto>>> GetAsync(
      string token = null,
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
