

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
