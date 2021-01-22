// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Dialogs.IViewService
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

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
