// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Providers.CachedUpdateInfoProvider
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure.Threading;
using DiShare.Logic.Updater.Checkers;
using DiShare.Logic.Updater.Models;
using System.Threading.Tasks;

namespace DiShare.Logic.Updater.Providers
{
  public class CachedUpdateInfoProvider : IUpdateInfoProvider
  {
    private readonly IUpdateChecker updateChecker;
    private readonly AsyncLock asyncLock;
    private UpdateInfo updateInfo;

    public CachedUpdateInfoProvider(IUpdateChecker updateChecker)
    {
      this.updateChecker = updateChecker;
      this.asyncLock = new AsyncLock();
    }

    public async Task<UpdateInfo> GetUpdateInfoAsync()
    {
      if (this.updateInfo == null)
      {
        using (await this.asyncLock.LockAsync())
        {
          if (this.updateInfo == null)
            this.updateInfo = (await this.updateChecker.GetUpdateInfoAsync().ConfigureAwait(false)).Value;
        }
      }
      return this.updateInfo;
    }
  }
}
