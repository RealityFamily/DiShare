

using Grace.DependencyInjection;
using DiShare.Wpf.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DiShare.Wpf.Navigation
{
  [ExcludeFromCodeCoverage]
  public class NavigationService : INavigationService
  {
    private readonly ILocatorService _locatorService;
    private readonly Stack<Tuple<Type, object, WeakReference>> _backStack;
    private Frame _frame;

    public Frame Frame
    {
      get => this._frame;
      set
      {
        this._frame = value;
        this._frame.NavigationService.LoadCompleted += new LoadCompletedEventHandler(this.NavigationService_LoadCompleted);
        this._frame.Navigating += new NavigatingCancelEventHandler(this.NavigationService_Navigating);
      }
    }

    private void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
    {
      if (e.NavigationMode != NavigationMode.Back)
        return;
      e.Cancel = true;
      this.GoBack();
    }

    public NavigationService(ILocatorService locatorService)
    {
      this._locatorService = locatorService;
      this._backStack = new Stack<Tuple<Type, object, WeakReference>>();
    }

    public void Navigate(Type type, object param)
    {
      if (this._backStack.Any<Tuple<Type, object, WeakReference>>())
      {
        Tuple<Type, object, WeakReference> tuple = this._backStack.Peek();
        if (tuple.Item3.IsAlive && tuple.Item3?.Target is IDisposable target)
        {
          if (tuple.Item1 == type && param == tuple.Item2)
            return;
          target.Dispose();
        }
        tuple.Item3.Target = (object) null;
      }
      object obj = this._locatorService.Locate(type);
      this._backStack.Push(new Tuple<Type, object, WeakReference>(type, param, new WeakReference(obj)));
      if (this.Frame == null)
        return;
      if (obj is Base.ViewModel viewModel)
        this.Frame.Navigate((object) viewModel.View, param);
      else
        this.Frame.Navigate(obj, param);
    }

    public void GoBack()
    {
      if (!this.CanGoBack())
        return;
      Tuple<Type, object, WeakReference> tuple1 = this._backStack.Pop();
      Tuple<Type, object, WeakReference> tuple2 = this._backStack.Peek();
      if (tuple2.Item2 == tuple1.Item2 && tuple2.Item1 == tuple1.Item1)
        return;
      object content = this._locatorService.Locate(tuple2.Item1);
      if (content is Base.ViewModel viewModel)
      {
        if (viewModel.View is NavigablePage view)
        {
          view.OnNavigatedBack(tuple2.Item2);
          this.Frame.Navigate((object) viewModel.View, tuple2.Item2);
        }
        else
          this.Frame.Navigate((object) viewModel.View, tuple2.Item2);
      }
      else
        this.Frame.Navigate(content, tuple2.Item2);
    }

    public bool CanGoBack() => this._backStack.Count > 1;

    private void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
    {
      if (!(e.Content is NavigablePage content))
        return;
      content.OnNavigated(e.ExtraData);
    }
  }
}
