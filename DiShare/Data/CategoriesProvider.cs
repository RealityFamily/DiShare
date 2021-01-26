
using DiShare.Data.Exceptions;
using DiShare.Data.Extensions;
using DiShare.Data.Repository;
using DiShare.Domain.DTO;
using DiShare.Domain.Entities;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Exceptions;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using DiShare.Services.Categories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiShare.Data
{
  public class CategoriesProvider : ICategoriesProvider
  {
    private readonly IItemDetector itemDetector;
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly ICategoriesService _categoriesService;
    private string directory;
    private ObservableCollection<CategoryItem> _categories = new ObservableCollection<CategoryItem>();
    private AsyncLock _asyncLock;

    public CategoriesProvider(
      IItemDetector itemDetector,
      ICategoriesRepository categoriesRepository,
      ICategoriesService categoriesService)
    {
      this.itemDetector = itemDetector;
      this._categoriesRepository = categoriesRepository;
      this._categoriesService = categoriesService;
      this._asyncLock = new AsyncLock();
    }

    public async Task InitCacheAsync()
    {
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        TryResult<IReadOnlyCollection<CategoryDto>> result = await this._categoriesService.GetAsync().ConfigureAwait(false);
        if (!result.IsFaulted)
        {
          this._categories.Clear();
          await this.UpdateCacheAsync(result.Value);
          this.FillCollection(this._categories, result.Value, new int?());
        }
        result = new TryResult<IReadOnlyCollection<CategoryDto>>();
      }
    }

    public async Task<TryResult<ICollection<CategoryItem>>> GetItemsAsync()
    {
      if (!this._categories.Any<CategoryItem>())
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        {
          if (!this._categories.Any<CategoryItem>())
          {
            try
            {
              TryResult<IReadOnlyCollection<CategoryDto>> result = await this._categoriesService.GetAsync().ConfigureAwait(false);
              if (!result.IsFaulted)
              {
                await this.UpdateCacheAsync(result.Value);
                this.FillCollection(this._categories, result.Value, new int?());
              }
              else
                this.FillCollection(this._categories, (IReadOnlyCollection<CategoryDto>) (await this._categoriesRepository.GetCategoriesAsync().ConfigureAwait(false)).Select<Category, CategoryDto>((Func<Category, CategoryDto>) (i => i.ToDto())).ToArray<CategoryDto>(), new int?());
              result = new TryResult<IReadOnlyCollection<CategoryDto>>();
            }
            catch (Exception ex)
            {
              return new TryResult<ICollection<CategoryItem>>(ex);
            }
          }
        }
      }
      return new TryResult<ICollection<CategoryItem>>((ICollection<CategoryItem>) this._categories);
    }

    private async Task UpdateCacheAsync(IReadOnlyCollection<CategoryDto> items)
    {
      try
      {
        if (!items.Any<CategoryDto>())
          return;
        IEnumerable<Category> cached = await this._categoriesRepository.GetCategoriesAsync().ConfigureAwait(false);
        int[] newIds = items.Select<CategoryDto, int>((Func<CategoryDto, int>) (i => i.Id)).ToArray<int>();
        int[] numArray = cached.Where<Category>((Func<Category, bool>) (i => !((IEnumerable<int>) newIds).Contains<int>(i.Id))).Select<Category, int>((Func<Category, int>) (i => i.Id)).ToArray<int>();
        int index;
        for (index = 0; index < numArray.Length; ++index)
          await this._categoriesRepository.DeleteCategoryAsync(numArray[index]).ConfigureAwait(false);
        numArray = (int[]) null;
        Category[] categoryArray = cached.Where<Category>((Func<Category, bool>) (i => ((IEnumerable<int>) newIds).Contains<int>(i.Id))).ToArray<Category>();
        for (index = 0; index < categoryArray.Length; ++index)
        {
          Category category = categoryArray[index];
          CategoryDto category1 = items.FirstOrDefault<CategoryDto>((Func<CategoryDto, bool>) (i => i.Id == category.Id));
          if (category1 != null)
          {
            if (category1.Name.Equals(category.Name))
            {
              int? categoryId1 = category1.CategoryId;
              int? categoryId2 = category.CategoryId;
              if (categoryId1.GetValueOrDefault() == categoryId2.GetValueOrDefault() & categoryId1.HasValue == categoryId2.HasValue)
                continue;
            }
            await this._categoriesRepository.UpdateCategoryAsync(category.Id, category1.ToEntity()).ConfigureAwait(false);
          }
        }
        categoryArray = (Category[]) null;
        int[] cachedIds = cached.Select<Category, int>((Func<Category, int>) (i => i.Id)).ToArray<int>();
        CategoryDto[] categoryDtoArray = items.Where<CategoryDto>((Func<CategoryDto, bool>) (i => !((IEnumerable<int>) cachedIds).Contains<int>(i.Id))).ToArray<CategoryDto>();
        for (index = 0; index < categoryDtoArray.Length; ++index)
          await this._categoriesRepository.AddCategoryAsync(categoryDtoArray[index].ToEntity()).ConfigureAwait(false);
        categoryDtoArray = (CategoryDto[]) null;
        cached = (IEnumerable<Category>) null;
      }
      catch (Exception ex)
      {
      }
    }

    private void FillCollection(
      ObservableCollection<CategoryItem> root,
      IReadOnlyCollection<CategoryDto> items,
      int? parentId)
    {
      foreach (CategoryItem categoryItem in items.Where<CategoryDto>((Func<CategoryDto, bool>) (i =>
      {
        int? categoryId = i.CategoryId;
        int? nullable = parentId;
        return categoryId.GetValueOrDefault() == nullable.GetValueOrDefault() & categoryId.HasValue == nullable.HasValue;
      })).Select<CategoryDto, CategoryItem>((Func<CategoryDto, CategoryItem>) (i => i.ToModel())).ToArray<CategoryItem>())
      {
        int result;
        int.TryParse(categoryItem.Id, out result);
        this.FillCollection(categoryItem.Categories, items, new int?(result));
        root.Add(categoryItem);
      }
    }

    public async Task<TryResult<Manifest>> GetBaseManifestAsync(string baseName)
    {
        if (!this.HasBase(baseName))
            return new TryResult<Manifest>((Exception) new RepositoryException("Base " + baseName + " not found"));
        try
        {
            using (StreamReader stream = new StreamReader(Path.Combine(this.directory, baseName + ".manifest")))
                return new TryResult<Manifest>(JsonConvert.DeserializeObject<Manifest>(await stream.ReadToEndAsync().ConfigureAwait(false)));
        }
        catch (Exception ex)
        {
            return new TryResult<Manifest>(ex);
        }
    }

    public bool HasStarterBase()
    {
      if (this.directory == null)
        throw new LibraryException("Library directory hasn't initialized yet");
      return Directory.Exists(this.directory) && File.Exists(Path.Combine(this.directory, "starter.manifest"));
    }

    public bool HasExtendedBase()
    {
      if (this.directory == null)
        throw new LibraryException("Library directory hasn't initialized yet");
      return Directory.Exists(this.directory) && File.Exists(Path.Combine(this.directory, "extended.manifest"));
    }

    public bool HasBase(string baseName)
    {
      if (this.directory == null)
        throw new LibraryException("Library directory hasn't initialized yet");
      return Directory.Exists(this.directory) && File.Exists(Path.Combine(this.directory, baseName + ".manifest"));
    }

    public void RemoveBase(string baseName)
    {
      if (this.directory == null)
        throw new LibraryException("Library directory hasn't initialized yet");
      if (!this.HasBase(baseName))
        return;
      Path.Combine(this.directory, baseName + ".manifest").RemoveFileSafe();
    }

    public IReadOnlyCollection<string> GetBases()
    {
      List<string> stringList = new List<string>();
      if (this.HasBase("internal"))
        stringList.Add("internal");
      return (IReadOnlyCollection<string>) stringList;
    }

    public string BasePath
    {
      get => this.directory;
      set => this.directory = value;
    }
  }
}
