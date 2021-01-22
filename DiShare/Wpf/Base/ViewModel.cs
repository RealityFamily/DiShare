// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Base.ViewModel
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;

namespace DiShare.Wpf.Base
{
  public abstract class ViewModel : ViewModelBase, IViewModel, INotifyPropertyChanged
  {
    private FrameworkElement _view;

    public abstract object Header { get; }

    public virtual FrameworkElement View
    {
      get => this._view;
      set
      {
        if (object.Equals((object) this._view, (object) value))
          return;
        this._view = value;
        if (this._view != null)
          this._view.DataContext = (object) this;
        this.RaisePropertyChanged(nameof (View));
      }
    }
  }
}
