
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace DiShare.Wpf.Converters
{
  public class BoolToVisibilityConverter : MarkupConverterBase
  {
    [ConstructorArgument("TrueValue")]
    public Visibility TrueValue { get; set; }

    [ConstructorArgument("FalseValue")]
    public Visibility FalseValue { get; set; }

    [ConstructorArgument("NullValue")]
    public Visibility NullValue { get; set; }

    public BoolToVisibilityConverter()
    {
      this.TrueValue = Visibility.Visible;
      this.FalseValue = Visibility.Collapsed;
      this.NullValue = Visibility.Collapsed;
    }

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (value == null)
        return (object) this.NullValue;
      return !(value is bool flag) ? (object) null : (object) (Visibility) (flag ? (int) this.TrueValue : (int) this.FalseValue);
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
