// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.NullToVisibilityConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace DiShare.Wpf.Converters
{
  public class NullToVisibilityConverter : MarkupConverterBase
  {
    [ConstructorArgument("NullValue")]
    public Visibility NullValue { get; set; }

    [ConstructorArgument("NotNullValue")]
    public Visibility NotNullValue { get; set; }

    public NullToVisibilityConverter()
    {
      this.NullValue = Visibility.Collapsed;
      this.NotNullValue = Visibility.Visible;
    }

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (Visibility) (value == null ? (int) this.NullValue : (int) this.NotNullValue);
    }

    protected override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
