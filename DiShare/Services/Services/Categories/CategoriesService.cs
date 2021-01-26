
using DiShare.Api.Base;
using DiShare.Api.Categories;
using DiShare.Api.Categories.Responses;
using DiShare.Domain.DTO;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Network;
using DiShare.Logic.Exceptions;
using DiShare.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Services.Categories
{
  public class CategoriesService : ICategoriesService
  {
    private readonly ICategoriesApi _categoriesApi;
    private readonly INetworkChecker _networkChecker;

    public CategoriesService(ICategoriesApi categoriesApi, INetworkChecker networkChecker)
    {
      this._categoriesApi = categoriesApi;
      this._networkChecker = networkChecker;
    }

    public async Task<TryResult<IReadOnlyCollection<CategoryDto>>> GetAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      if (!this._networkChecker.CheckIsConnected())
        return new TryResult<IReadOnlyCollection<CategoryDto>>((Exception) new CategoriesServiceException("Network connection error."));
      try
      {
        Response<IReadOnlyCollection<CategoryResponse>> response = await this._categoriesApi.GetCategoriesAsync(cancellationToken).ConfigureAwait(false);
        return response.Status == ResponseStatus.Error || !response.Error.IsNullOrEmpty() ? new TryResult<IReadOnlyCollection<CategoryDto>>((Exception) new CategoriesServiceException(string.Format("Retrieve categories error: [{0}] {1}", (object) response.Status, (object) response.Error))) : new TryResult<IReadOnlyCollection<CategoryDto>>((IReadOnlyCollection<CategoryDto>) response.Result.Select<CategoryResponse, CategoryDto>((Func<CategoryResponse, CategoryDto>) (i => i.ToDto())).ToArray<CategoryDto>());
      }
      catch (Exception ex)
      {
        return new TryResult<IReadOnlyCollection<CategoryDto>>((Exception) new CategoriesServiceException("Can't retrieve categories from server", ex));
      }
    }
  }
}
