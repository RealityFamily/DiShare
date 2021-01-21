// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.ExecutionTracker
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;
using System.Threading;

namespace Library.Infrastructure
{
  public class ExecutionTracker
  {
    private readonly Action _onSuspend;
    private readonly Action _onResume;
    private bool _isBusy;
    private int _nestingLevel;
    private CancellationTokenSource _cancellationSource;

    public bool IsBusy => Volatile.Read(ref this._isBusy);

    public ExecutionTracker() => this._cancellationSource = new CancellationTokenSource();

    public ExecutionTracker(Action onSuspend, Action onResume)
      : this()
    {
      this._onSuspend = onSuspend;
      this._onResume = onResume;
    }

    public ExecutionTracker.ExecutionSuspenderContext TrackExecution() => new ExecutionTracker.ExecutionSuspenderContext(this, this._cancellationSource.Token);

    public ExecutionTracker.ExecutionSuspenderContext TrackWithReset()
    {
      this.RecreateAndCancel();
      return new ExecutionTracker.ExecutionSuspenderContext(this, this._cancellationSource.Token);
    }

    private void RecreateAndCancel()
    {
      this._cancellationSource.Cancel();
      this._cancellationSource = new CancellationTokenSource();
      this._nestingLevel = 0;
    }

    public class ExecutionSuspenderContext : IDisposable
    {
      private readonly ExecutionTracker _executionTracker;
      private readonly CancellationToken _token;

      public ExecutionSuspenderContext(ExecutionTracker executionTracker, CancellationToken token)
      {
        if (this._token.IsCancellationRequested)
          return;
        this._executionTracker = executionTracker;
        this._token = token;
        this._executionTracker._isBusy = true;
        if (Interlocked.Increment(ref this._executionTracker._nestingLevel) != 1)
          return;
        this._executionTracker = executionTracker;
        Action onSuspend = this._executionTracker._onSuspend;
        if (onSuspend == null)
          return;
        onSuspend();
      }

      public void Dispose()
      {
        if (this._token.IsCancellationRequested || Interlocked.Decrement(ref this._executionTracker._nestingLevel) != 0)
          return;
        this._executionTracker._isBusy = false;
        Action onResume = this._executionTracker._onResume;
        if (onResume == null)
          return;
        onResume();
      }
    }
  }
}
