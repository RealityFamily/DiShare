

using DiShare.Api.Categories.Responses;
using DiShare.Domain.DTO;

namespace DiShare.Services.Extensions
{
  public static class CategoryExtension
  {
    public static CategoryDto ToDto(this CategoryResponse category) => new CategoryDto()
    {
      Id = category.Id,
      Name = category.Name,
      CategoryId = category.CategoryId
    };
  }
}
