

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
