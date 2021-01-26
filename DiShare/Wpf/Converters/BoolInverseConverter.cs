

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
