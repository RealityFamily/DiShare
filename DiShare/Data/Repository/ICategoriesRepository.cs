// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Repository.ICategoriesRepository
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiShare.Data.Repository
{
  public interface ICategoriesRepository
  {
    Task<IEnumerable<Category>> GetCategoriesAsync();

    Task<Category> GetCategoryByIdAsync(int id);

    Task AddCategoryAsync(Category category);

    Task UpdateCategoryAsync(int id, Category category);

    Task DeleteCategoryAsync(int id);
  }
}
