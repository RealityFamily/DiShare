

using GalaSoft.MvvmLight.Messaging;
using Grace.DependencyInjection;
using DiShare.Wpf.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DiShare.Wpf.Dialogs
{
  public class ViewService : IViewService
  {
    private readonly ILocatorService _locatorService;
    private readonly List<Window> _openedWindows;

    public ViewService(IMessenger messenger, ILocatorService locatorService)
    {
      this._locatorService = locatorService;
      this._openedWindows = new List<Window>();
      messenger.Register<RequestCloseMessage>((object) this, new Action<RequestCloseMessage>(this.OnRequestClose));
    }

    [DebuggerStepThrough]
    public void OpenWindow<TViewModel>() where TViewModel : Base.ViewModel => this.CreateWindow<TViewModel>(WindowMode.Window).Show();

    [DebuggerStepThrough]
    public void OpenWindow<TViewModel>(TViewModel viewModel) where TViewModel : Base.ViewModel => this.CreateWindow<TViewModel>(viewModel, WindowMode.Window).Show();

    [DebuggerStepThrough]
    public bool? OpenDialog<TViewModel>() where TViewModel : Base.ViewModel => this.CreateWindow<TViewModel>(WindowMode.Dialog).ShowDialog();

    [DebuggerStepThrough]
    public bool? OpenDialog<TViewModel>(TViewModel viewModel) where TViewModel : Base.ViewModel => this.CreateWindow<TViewModel>(viewModel, WindowMode.Dialog).ShowDialog();

    [DebuggerStepThrough]
    public Window CreateWindow<TViewModel>(WindowMode windowMode) where TViewModel : Base.ViewModel => this.CreateWindow<TViewModel>(this._locatorService.Locate<TViewModel>(), windowMode);

    [DebuggerStepThrough]
    public Window CreateWindow<TViewModel>(TViewModel viewModel, WindowMode windowMode) where TViewModel : Base.ViewModel
    {
      Window view = (Window) viewModel.View;
      view.DataContext =  (object) viewModel;
      view.Closed += new EventHandler(this.OnClosed);
      lock (this._openedWindows)
      {
        if (windowMode == WindowMode.Dialog && this._openedWindows.Count > 0)
        {
          Window openedWindow = this._openedWindows[this._openedWindows.Count - 1];
          if (openedWindow.IsActive && !object.Equals((object) view, (object) openedWindow))
            view.Owner = openedWindow;
        }
        this._openedWindows.Add(view);
      }
      return view;
    }

    public int GetOpenedWindowsCount()
    {
      lock (this._openedWindows)
        return this._openedWindows.Count;
    }

    private void OnRequestClose(RequestCloseMessage message)
    {
      Window window = this._openedWindows.SingleOrDefault<Window>((Func<Window, bool>) (w => w.DataContext == message.ViewModel));
      if (window == null)
        return;
      if (message.DialogResult.HasValue)
        window.DialogResult = message.DialogResult;
      else
        window.Close();
    }

    private void OnClosed(object sender, EventArgs e)
    {
      Window window = (Window) sender;
      window.Closed -= new EventHandler(this.OnClosed);
      lock (this._openedWindows)
        this._openedWindows.Remove(window);
    }
  }
}
