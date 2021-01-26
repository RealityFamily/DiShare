

using DiShare.Api.Images;
using DiShare.Data.CacheFolderProvider;
using DiShare.Data.Exceptions;
using DiShare.Data.Extensions;
using DiShare.Data.ImageValidator;
using DiShare.Data.Repository;
using DiShare.Domain.DTO;
using DiShare.Domain.Entities;
using DiShare.Domain.Enums;
using DiShare.Domain.Models;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using DiShare.Services.Items;
using DiShare.Wpf.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DiShare.Data.Providers
{
  public class ItemsProvider : IItemsProvider
  {
    private readonly IItemsRepository _itemsRepository;
    private readonly IItemsService _itemsService;
    private readonly IVendorsRepository _vendorsRepository;
    private readonly ICacheFolderProvider _cacheFolderProvider;
    private readonly IItemDetector _itemDetector;
    private readonly IImageApi _imageApi;
    private readonly IDispatcherHelper _dispatcherHelper;
    private readonly IImageValidator _imageValidator;
    private IEnumerable<VendorDto> _vendors;
    private readonly AsyncLock _asyncLock;

    public ItemsProvider(
      IItemsRepository itemsRepository,
      IItemsService itemsService,
      IVendorsRepository vendorsRepository,
      ICacheFolderProvider cacheFolderProvider,
      IItemDetector itemDetector,
      IImageApi imageApi,
      IDispatcherHelper dispatcherHelper,
      IImageValidator imageValidator)
    {
      this._itemsRepository = itemsRepository;
      this._itemsService = itemsService;
      this._vendorsRepository = vendorsRepository;
      this._cacheFolderProvider = cacheFolderProvider;
      this._itemDetector = itemDetector;
      this._imageApi = imageApi;
      this._dispatcherHelper = dispatcherHelper;
      this._imageValidator = imageValidator;
      this._asyncLock = new AsyncLock();
    }

    public async Task InitCacheAsync(string token = null)
    {
      TryResult<IReadOnlyCollection<ItemDto>> tryResult = await this._itemsService.GetAsync(token).ConfigureAwait(false);
      ConfiguredTaskAwaitable configuredTaskAwaitable;
      if (!tryResult.IsFaulted)
      {
        configuredTaskAwaitable = this.UpdateCacheAsync(tryResult.Value).ConfigureAwait(false);
        await configuredTaskAwaitable;
      }
      configuredTaskAwaitable = this.InitVendorsAsync().ConfigureAwait(false);
      await configuredTaskAwaitable;
    }

    private async Task InitVendorsAsync()
    {
      if (this._vendors != null)
        return;
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        if (this._vendors == null)
          this._vendors = (IEnumerable<VendorDto>) (await this._vendorsRepository.GetVendorsAsync().ConfigureAwait(false)).Select<Vendor, VendorDto>((Func<Vendor, VendorDto>) (i => i.ToDto())).ToArray<VendorDto>();
      }
    }

    private async Task UpdateCacheAsync(IReadOnlyCollection<ItemDto> items)
    {
      try
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        {
          IEnumerable<Item> cached = await this._itemsRepository.GetItemsAsync().ConfigureAwait(false);
          string[] newIds = items.Select<ItemDto, string>((Func<ItemDto, string>) (i => i.Id)).ToArray<string>();
          string[] strArray = cached.Where<Item>((Func<Item, bool>) (i => !((IEnumerable<string>) newIds).Contains<string>(i.Id))).Select<Item, string>((Func<Item, string>) (i => i.Id)).ToArray<string>();
          int index;
          for (index = 0; index < strArray.Length; ++index)
            await this._itemsRepository.DeleteItemAsync(strArray[index]).ConfigureAwait(false);
          strArray = (string[]) null;
          Item[] objArray = cached.Where<Item>((Func<Item, bool>) (i => ((IEnumerable<string>) newIds).Contains<string>(i.Id))).ToArray<Item>();
          for (index = 0; index < objArray.Length; ++index)
          {
            Item item = objArray[index];
            ItemDto itemDto = items.FirstOrDefault<ItemDto>((Func<ItemDto, bool>) (i => i.Id == item.Id));
            if (itemDto != null && !itemDto.Equals((object) item))
            {
              item.Name = itemDto.Name;
              item.Size = itemDto.Size;
              item.CategoryId = itemDto.CategoryId;
              item.Description = itemDto.Description;
              item.ForRegistered = itemDto.ForRegistered;
              item.Hash = itemDto.Hash;
              item.ItemType = itemDto.ItemType.ToString();
              item.VendorId = itemDto.VendorId;
              item.Price = itemDto.Price;
              item.Url = itemDto.Url;
              item.Version = itemDto.Version;
              await this._itemsRepository.UpdateItemAsync(item.Id, item).ConfigureAwait(false);
            }
          }
          objArray = (Item[]) null;
          string[] cachedIds = cached.Select<Item, string>((Func<Item, string>) (i => i.Id)).ToArray<string>();
          ItemDto[] itemDtoArray = items.Where<ItemDto>((Func<ItemDto, bool>) (i => !((IEnumerable<string>) cachedIds).Contains<string>(i.Id))).ToArray<ItemDto>();
          for (index = 0; index < itemDtoArray.Length; ++index)
            await this._itemsRepository.AddItemAsync(itemDtoArray[index].ToEntity()).ConfigureAwait(false);
          itemDtoArray = (ItemDto[]) null;
          cached = (IEnumerable<Item>) null;
        }
      }
      catch (Exception ex)
      {
      }
    }

    public async Task<TryResult<IReadOnlyCollection<ModelItem>>> GetItemsAsync(
      string categoryId)
    {
      TryResult<string> folderResult = await this._cacheFolderProvider.GetFolderAsync();
      if (folderResult.IsFaulted)
        return new TryResult<IReadOnlyCollection<ModelItem>>((Exception) new CacheException("Cache is not initialized"));
      int id;
      if (!int.TryParse(categoryId, out id))
        return new TryResult<IReadOnlyCollection<ModelItem>>((Exception) new ProviderException("Can't parse categoryId"));
      if (this._vendors == null)
        await this.InitVendorsAsync().ConfigureAwait(false);
      try
      {
        List<ModelItem> models = (List<ModelItem>) null;
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
          models = (await this._itemsRepository.GetItemsByCategoryIdAsync(id).ConfigureAwait(false)).Select<Item, ModelItem>((Func<Item, ModelItem>) (i => i.ToModel(this._vendors, folderResult.Value))).ToList<ModelItem>();
        foreach (ModelItem modelItem in models)
        {
          bool flag = this._itemDetector.IsItemFolder(modelItem.Path);
          if (flag && modelItem.State == ItemState.NotCached || !flag && modelItem.State == ItemState.Cached)
          {
            modelItem.State = flag ? ItemState.Cached : ItemState.NotCached;
            TryResult tryResult = await this.UpdateItemAsync(modelItem).ConfigureAwait(false);
          }
        }
        return new TryResult<IReadOnlyCollection<ModelItem>>((IReadOnlyCollection<ModelItem>) models);
      }
      catch (Exception ex)
      {
        return new TryResult<IReadOnlyCollection<ModelItem>>(ex);
      }
    }

    public async Task<TryResult<ModelItem>> GetItemByIdAsync(string id)
    {
      TryResult<string> folderResult = await this._cacheFolderProvider.GetFolderAsync();
      if (folderResult.IsFaulted)
        return new TryResult<ModelItem>((Exception) new CacheException("Cache is not initialized"));
      try
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
          return new TryResult<ModelItem>((await this._itemsRepository.GetItemsByIdAsync(id).ConfigureAwait(false)).ToModel(this._vendors, folderResult.Value));
      }
      catch (Exception ex)
      {
        return new TryResult<ModelItem>(ex);
      }
    }

    public async Task<TryResult<IReadOnlyCollection<ModelItem>>> GetItemsAsync()
    {
      TryResult<string> folderResult = await this._cacheFolderProvider.GetFolderAsync();
      if (folderResult.IsFaulted)
        return new TryResult<IReadOnlyCollection<ModelItem>>((Exception) new CacheException("Cache is not initialized"));
      try
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
          return new TryResult<IReadOnlyCollection<ModelItem>>((IReadOnlyCollection<ModelItem>) (await this._itemsRepository.GetItemsAsync().ConfigureAwait(false)).Select<Item, ModelItem>((Func<Item, ModelItem>) (i => i.ToModel(this._vendors, folderResult.Value))).ToArray<ModelItem>());
      }
      catch (Exception ex)
      {
        return new TryResult<IReadOnlyCollection<ModelItem>>(ex);
      }
    }

    public async Task<TryResult<ModelItem>> GetImagesAsync(ModelItem item)
    {
      TryResult<string> tryResult = await this._cacheFolderProvider.GetFolderAsync().ConfigureAwait(false);
      if (tryResult.IsFaulted)
        return new TryResult<ModelItem>((Exception) new CacheException("Cache is not initialized"));
      string str = Path.Combine(tryResult.Value, "Cache", item.Id, "Images");
      str.CreateDirectoryIfMissing();
      string small = Path.Combine(str, "170.jpg");
      string large = Path.Combine(str, "640.jpg");
      bool flag = await this.CheckImageFile(small);
      if (flag)
        flag = await this.CheckImageFile(large);
      if (flag)
      {
        this._dispatcherHelper.CheckBeginInvokeOnUI((Action) (() =>
        {
          item.SmallImage = small;
          item.Image = large;
        }));
      }
      else
      {
        try
        {
          if (!await this.CheckImageFile(small))
          {
            if (!(await this.DownloadImage(item.Id, small, ImageSize.Small).ConfigureAwait(false)).IsFaulted)
              this._dispatcherHelper.CheckBeginInvokeOnUI((Action) (() => item.SmallImage = small));
          }
          if (!await this.CheckImageFile(large))
          {
            if (!(await this.DownloadImage(item.Id, large, ImageSize.Large).ConfigureAwait(false)).IsFaulted)
              this._dispatcherHelper.CheckBeginInvokeOnUI((Action) (() => item.Image = large));
          }
        }
        catch (Exception ex)
        {
          return new TryResult<ModelItem>(ex);
        }
      }
      return new TryResult<ModelItem>(item);
    }

    private async Task<bool> CheckImageFile(string file)
    {
      if (!File.Exists(file))
        return false;
      TryResult<bool> tryResult = await this._imageValidator.CheckContentForImageAsync(file).ConfigureAwait(false);
      return !tryResult.IsFaulted && tryResult.Value;
    }

    public async Task<TryResult> UpdateItemAsync(ModelItem item)
    {
      try
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        {
          Item obj = await this._itemsRepository.GetItemsByIdAsync(item.Id).ConfigureAwait(false);
          obj.State = (int) item.State;
          await this._itemsRepository.UpdateItemAsync(obj.Id, obj).ConfigureAwait(false);
        }
        return new TryResult();
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
    }

    private async Task<TryResult> DownloadImage(
      string id,
      string filename,
      ImageSize size)
    {
      string tempFileName = Path.GetTempFileName();
      filename.RemoveFileSafe();
      int attempt = 3;
      TryResult result;
      do
      {
        result = await this._imageApi.TryDownloadImageAsync(id, tempFileName, size).ConfigureAwait(false);
        if (!result.IsFaulted)
        {
          if (!(await this._imageValidator.CheckContentForImageAsync(tempFileName).ConfigureAwait(false)).IsFaulted)
          {
            try
            {
              File.Move(tempFileName, filename);
              break;
            }
            catch (Exception ex)
            {
              return new TryResult(ex);
            }
          }
        }
      }
      while (!result.IsFaulted && --attempt > 0);
      tempFileName.RemoveFileSafe();
      return result;
    }

    public async Task<TryResult> ClearCacheItem(ModelItem item)
    {
      try
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        {
          if (item.State != ItemState.Cached)
            return new TryResult();
          string path2 = "";
          switch (item.Type)
          {
            case ItemType.Model:
              path2 = "Model";
              break;
            case ItemType.Material:
              path2 = "MaxMaterial";
              break;
            case ItemType.Asset:
              path2 = "Asset";
              break;
          }
          Path.Combine(item.Path, path2).RemoveDirectorySafe();
          item.State = ItemState.NotCached;
        }
        TryResult tryResult = await this.UpdateItemAsync(item);
        return new TryResult();
      }
      catch (Exception ex)
      {
        return new TryResult(ex);
      }
    }
  }
}
