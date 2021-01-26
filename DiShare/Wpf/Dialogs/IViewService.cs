

using DiShare.Wpf.Base;
using System.Windows;

namespace DiShare.Wpf.Dialogs
{
  public interface IViewService
  {
    void OpenWindow<TViewModel>() where TViewModel : Base.ViewModel;

    void OpenWindow<TViewModel>(TViewModel viewModel) where TViewModel : Base.ViewModel;

    bool? OpenDialog<TViewModel>() where TViewModel : Base.ViewModel;

    bool? OpenDialog<TViewModel>(TViewModel viewModel) where TViewModel : Base.ViewModel;

    Window CreateWindow<TViewModel>(WindowMode windowMode) where TViewModel : Base.ViewModel;

    Window CreateWindow<TViewModel>(TViewModel viewModel, WindowMode windowMode) where TViewModel : Base.ViewModel;

    int GetOpenedWindowsCount();
  }
}
