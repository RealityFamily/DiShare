// Decompiled with JetBrains decompiler
// Type: DiShare.Api.Categories.ICategoriesApi
// Assembly: DiShare.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D1FF48D2-FED5-4347-AD95-28516C9FD1F5
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Api.dll

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
