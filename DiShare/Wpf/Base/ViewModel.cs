
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
