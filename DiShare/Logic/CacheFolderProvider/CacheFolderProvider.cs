// Decompiled with JetBrains decompiler
// Type: Logic.CacheFolderProvider.CacheFolderProvider
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Data.CacheFolderProvider;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using DiShare.Logic.RegistrySettingsProvider;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiShare.Logic.CacheFolderProvider
{
  public class CacheFolderProvider : ICacheFolderProvider
  {
    private readonly IRegistrySettingsProvider _registrySettingsProvider;
    private readonly AsyncLock _asyncLock;
    private string _folder;

    public CacheFolderProvider(IRegistrySettingsProvider registrySettingsProvider)
    {
      this._registrySettingsProvider = registrySettingsProvider;
      this._asyncLock = new AsyncLock();
    }

    public async Task<TryResult<string>> GetFolderAsync()
    {
      if (this._folder == null)
      {
        using (await this._asyncLock.LockAsync().ConfigureAwait(false))
        {
          if (this._folder == null)
            this._folder = await this._registrySettingsProvider.GetLibraryPathAsync().ConfigureAwait(false);
        }
      }
      return !this._folder.IsNullOrWhiteSpace() ? new TryResult<string>(this._folder) : new TryResult<string>((Exception) new DirectoryNotFoundException());
    }

    public async Task SetFolder(string path)
    {
      using (await this._asyncLock.LockAsync().ConfigureAwait(false))
      {
        this._folder = path;
        await this._registrySettingsProvider.SetLibraryPathAsync(path).ConfigureAwait(false);
      }
    }
  }
}
