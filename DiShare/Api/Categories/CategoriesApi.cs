// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Categories.CategoriesApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

using DiShare.Api.Abstract;
using DiShare.Api.Base;
using DiShare.Api.Categories.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace DiShare.Api.Categories
{
  public class CategoriesApi : ApiBase, ICategoriesApi
  {
    public CategoriesApi(TinyRestClient client)
      : base(client)
    {
    }

    public Task<Response<IReadOnlyCollection<CategoryResponse>>> GetCategoriesAsync(
      CancellationToken cancellationToken = default (CancellationToken))
    {
      return this.Client.GetRequest("api/v1/categories").AddHeader("X-API-KEY", this.ApiKey).ExecuteAsync<Response<IReadOnlyCollection<CategoryResponse>>>(cancellationToken);
    }
  }
}
