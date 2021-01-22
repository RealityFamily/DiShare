// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Helpers.PasswordHelper
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System.Windows;
using System.Windows.Controls;

namespace DiShare.Wpf.Helpers
{
  public static class PasswordHelper
  {
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof (string), typeof (PasswordHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, new PropertyChangedCallback(PasswordHelper.OnPasswordPropertyChanged)));
    public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof (bool), typeof (PasswordHelper), new PropertyMetadata((object) false, new PropertyChangedCallback(PasswordHelper.Attach)));
    private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof (bool), typeof (PasswordHelper));

    public static void SetAttach(DependencyObject dp, bool value) => dp.SetValue(PasswordHelper.AttachProperty, (object) value);

    public static bool GetAttach(DependencyObject dp) => (bool) dp.GetValue(PasswordHelper.AttachProperty);

    public static string GetPassword(DependencyObject dp) => (string) dp.GetValue(PasswordHelper.PasswordProperty);

    public static void SetPassword(DependencyObject dp, string value) => dp.SetValue(PasswordHelper.PasswordProperty, (object) value);

    private static bool GetIsUpdating(DependencyObject dp) => (bool) dp.GetValue(PasswordHelper.IsUpdatingProperty);

    private static void SetIsUpdating(DependencyObject dp, bool value) => dp.SetValue(PasswordHelper.IsUpdatingProperty, (object) value);

    private static void OnPasswordPropertyChanged(
      DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
    {
      PasswordBox passwordBox = sender as PasswordBox;
      passwordBox.PasswordChanged -= new RoutedEventHandler(PasswordHelper.PasswordChanged);
      if (!PasswordHelper.GetIsUpdating((DependencyObject) passwordBox))
        passwordBox.Password = (string) e.NewValue;
      passwordBox.PasswordChanged += new RoutedEventHandler(PasswordHelper.PasswordChanged);
    }

    private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    {
      if (!(sender is PasswordBox passwordBox))
        return;
      if ((bool) e.OldValue)
        passwordBox.PasswordChanged -= new RoutedEventHandler(PasswordHelper.PasswordChanged);
      if (!(bool) e.NewValue)
        return;
      passwordBox.PasswordChanged += new RoutedEventHandler(PasswordHelper.PasswordChanged);
    }

    private static void PasswordChanged(object sender, RoutedEventArgs e)
    {
      PasswordBox passwordBox = sender as PasswordBox;
      PasswordHelper.SetIsUpdating((DependencyObject) passwordBox, true);
      PasswordHelper.SetPassword((DependencyObject) passwordBox, passwordBox.Password);
      PasswordHelper.SetIsUpdating((DependencyObject) passwordBox, false);
    }
  }
}
