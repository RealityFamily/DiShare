

using System.ComponentModel;
using System.Windows;

namespace DiShare.Wpf.Base
{
  public interface IViewModel : INotifyPropertyChanged
  {
    object Header { get; }

    FrameworkElement View { get; }
  }
}
