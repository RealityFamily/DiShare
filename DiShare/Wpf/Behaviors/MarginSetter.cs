// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Behaviors.MarginSetter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

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
