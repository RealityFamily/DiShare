﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.BoolToVisibilityMultiConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace DiShare.Wpf.Converters
{
  public class BoolToVisibilityMultiConverter : MarkupMultiValueConverterBase
  {
    protected override object Convert(
      object[] values,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (Visibility) (values.OfType<bool>().All<bool>((Func<bool, bool>) (b => b)) ? 0 : 2);
    }

    protected override object[] ConvertBack(
      object value,
      Type[] targetTypes,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
