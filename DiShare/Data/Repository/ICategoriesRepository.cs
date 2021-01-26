
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
