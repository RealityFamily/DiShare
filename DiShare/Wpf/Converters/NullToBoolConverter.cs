
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
