﻿using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DiShare.Infrastructure.Threading
{
  public class AsyncLock
  {
    private readonly SemaphoreSlim _semaphoreSlim;
    private readonly AsyncLock.LockReleaser _lockReleaser;

    public AsyncLock()
      : this(1)
    {
    }

    public AsyncLock(int concurrencyLevel)
    {
      this._semaphoreSlim = new SemaphoreSlim(concurrencyLevel);
      this._lockReleaser = new AsyncLock.LockReleaser(this._semaphoreSlim);
    }

    public async Task<AsyncLock.LockReleaser> LockAsync()
    {
      await this._semaphoreSlim.WaitAsync();
      return this._lockReleaser;
    }

    public TaskAwaiter<AsyncLock.LockReleaser> GetAwaiter() => this.LockAsync().GetAwaiter();

    public sealed class LockReleaser : IDisposable
    {
      private readonly SemaphoreSlim _semaphoreSlim;

      internal LockReleaser(SemaphoreSlim semaphoreSlim) => this._semaphoreSlim = semaphoreSlim;

      void IDisposable.Dispose() => this._semaphoreSlim.Release();
    }
  }
}
