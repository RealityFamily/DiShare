

using GalaSoft.MvvmLight.Threading;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;

namespace DiShare.Wpf.Helpers
{
  [ExcludeFromCodeCoverage]
  public class DispatcherHelperAdapter : IDispatcherHelper
  {
    public DispatcherHelperAdapter() => DispatcherHelper.Initialize();

    public Dispatcher UIDispatcher => DispatcherHelper.UIDispatcher;

    public void CheckBeginInvokeOnUI(Action action) => DispatcherHelper.CheckBeginInvokeOnUI(action);

    public DispatcherOperation RunAsync(Action action) => DispatcherHelper.RunAsync(action);

    public void Reset() => DispatcherHelper.Reset();
  }
}
