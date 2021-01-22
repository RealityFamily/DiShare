// Decompiled with JetBrains decompiler
// Type: DiShare.Services.Extensions.CategoryExtension
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

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
