// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Categories.ICategoriesService
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

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
