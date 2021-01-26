

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DiShare.Wpf.Behaviors
{
  public class MarginSetter
  {
    public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached("Margin", typeof (Thickness), typeof (MarginSetter), (PropertyMetadata) new UIPropertyMetadata((object) new Thickness(), new PropertyChangedCallback(MarginSetter.MarginChangedCallback)));

    public static Thickness GetMargin(DependencyObject obj) => (Thickness) obj.GetValue(MarginSetter.MarginProperty);

    public static void SetMargin(DependencyObject obj, Thickness value) => obj.SetValue(MarginSetter.MarginProperty, (object) value);

    public static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is Panel panel))
        return;
      panel.Loaded += new RoutedEventHandler(MarginSetter.PanelLoaded);
    }

    private static void PanelLoaded(object sender, RoutedEventArgs e)
    {
      if (!(sender is Panel panel))
        throw new ArgumentNullException(nameof (sender));
      foreach (FrameworkElement frameworkElement in panel.Children.OfType<FrameworkElement>())
        frameworkElement.Margin = MarginSetter.GetMargin((DependencyObject) panel);
    }
  }
}
