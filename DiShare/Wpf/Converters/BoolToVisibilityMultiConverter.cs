

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
