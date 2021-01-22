﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.BoolToSolidColorBrushConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;

namespace DiShare.Wpf.Converters
{
  public class BoolToSolidColorBrushConverter : MarkupConverterBase
  {
    [ConstructorArgument("TrueValue")]
    public SolidColorBrush TrueValue { get; set; }

    [ConstructorArgument("FalseValue")]
    public SolidColorBrush FalseValue { get; set; }

    [ConstructorArgument("NullValue")]
    public SolidColorBrush NullValue { get; set; }

    public BoolToSolidColorBrushConverter()
    {
      this.TrueValue = new SolidColorBrush(Color.FromRgb((byte) 128, (byte) 128, (byte) 128));
      this.FalseValue = new SolidColorBrush(Color.FromRgb((byte) 0, (byte) 128, (byte) 128));
      this.NullValue = new SolidColorBrush(Color.FromRgb((byte) 128, (byte) 128, (byte) 0));
    }

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (value == null)
        return (object) this.NullValue;
      if (!(value is bool flag))
        return (object) null;
      return !flag ? (object) this.FalseValue : (object) this.TrueValue;
    }

    protected override object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (object.Equals(value, (object) this.TrueValue))
        return (object) true;
      return object.Equals(value, (object) this.FalseValue) ? (object) false : (object) null;
    }
  }
}