

using System;
using System.Windows.Controls;

namespace DiShare.Wpf.Navigation
{
  public interface INavigationService
  {
    Frame Frame { get; set; }

    void Navigate(Type type, object param);

    void GoBack();

    bool CanGoBack();
  }
}
