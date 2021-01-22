// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Converters.NullToBoolConverter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Globalization;
using System.Windows.Markup;

namespace DiShare.Wpf.Converters
{
  public class NullToBoolConverter : MarkupConverterBase
  {
    [ConstructorArgument("NullValue")]
    public bool NullValue { get; set; }

    [ConstructorArgument("NotNullValue")]
    public bool NotNullValue { get; set; }

    public NullToBoolConverter()
    {
      this.NullValue = false;
      this.NotNullValue = true;
    }

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      return (object) (bool) (value == null ? (this.NullValue ? true : false) : (this.NotNullValue ? true : false));
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
