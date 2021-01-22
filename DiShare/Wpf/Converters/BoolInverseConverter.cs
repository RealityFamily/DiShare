// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.BoolInverseConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;

namespace DiShare.Wpf.Converters
{
  public class BoolInverseConverter : MarkupConverterBase
  {
    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      bool? nullable = value as bool?;
      return (object) (nullable.HasValue ? new bool?(!nullable.GetValueOrDefault()) : new bool?());
    }

    protected override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return this.Convert(value, targetType, parameter, culture);
    }
  }
}
