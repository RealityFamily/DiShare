// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Extensions.CategoryExtension
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Api.Categories.Responses;
using DiShare.Domain.DTO;
using DiShare.Domain.Entities;
using DiShare.Domain.Models;

namespace DiShare.Data.Extensions
{
  public static class CategoryExtension
  {
    public static CategoryItem ToModel(this CategoryDto category)
    {
      CategoryItem categoryItem = new CategoryItem();
      categoryItem.Id = category.Id.ToString();
      categoryItem.Name = category.Name;
      return categoryItem;
    }

    public static CategoryItem ToModel(this CategoryResponse category)
    {
      CategoryItem categoryItem = new CategoryItem();
      categoryItem.Id = category.Id.ToString();
      categoryItem.Name = category.Name;
      return categoryItem;
    }

    public static CategoryDto ToDto(this Category category) => new CategoryDto()
    {
      Id = category.Id,
      Name = category.Name,
      CategoryId = category.CategoryId
    };

    public static Category ToEntity(this CategoryDto category) => new Category()
    {
      Id = category.Id,
      Name = category.Name,
      CategoryId = category.CategoryId
    };
  }
}
