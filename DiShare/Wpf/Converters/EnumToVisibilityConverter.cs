// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.EnumToVisibilityConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using DiShare.Infrastructure.Extensions;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace DiShare.Wpf.Converters
{
  public class EnumToVisibilityConverter : MarkupConverterBase
  {
    [ConstructorArgument("VisibleValue")]
    public object VisibleValue { get; set; }

    [ConstructorArgument("InverseValue")]
    public object InverseValue { get; set; } = (object) "false";

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (value == null || this.VisibleValue == null)
        return (object) Visibility.Collapsed;
      if (!(value is Enum))
        return (object) null;
      string inverseValue = (string) this.InverseValue;
      bool flag = inverseValue != null && inverseValue.IsEqualsIgnoreCase(true.ToString());
      return (object) (Visibility) (((Enum) value).ToString().IsEqualsIgnoreCase(this.VisibleValue.ToString()) ^ flag ? 0 : 2);
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
