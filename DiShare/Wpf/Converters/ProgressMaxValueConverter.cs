// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.ProgressMaxValueConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;

namespace DiShare.Wpf.Converters
{
  public class ProgressMaxValueConverter : MarkupConverterBase
  {
    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      long valueOrDefault = (value as long?).GetValueOrDefault();
      return (object) (valueOrDefault == 0L ? 100.0 : (double) valueOrDefault);
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
