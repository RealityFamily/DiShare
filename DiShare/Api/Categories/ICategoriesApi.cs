
using DiShare.Api.Base;
using DiShare.Api.Categories.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Api.Categories
{
  public interface ICategoriesApi
  {
    Task<Response<IReadOnlyCollection<CategoryResponse>>> GetCategoriesAsync(
      CancellationToken cancellationToken = default (CancellationToken));
  }
}
