
using System;
using System.Windows.Threading;

namespace DiShare.Wpf.Helpers
{
  public interface IDispatcherHelper
  {
    Dispatcher UIDispatcher { get; }

    void CheckBeginInvokeOnUI(Action action);

    DispatcherOperation RunAsync(Action action);

    void Reset();
  }
}
