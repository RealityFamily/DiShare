// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Items.IItemsService
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

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
