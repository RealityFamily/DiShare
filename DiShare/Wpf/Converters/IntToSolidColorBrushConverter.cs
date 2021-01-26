

using System;
using System.Globalization;
using System.Windows.Markup;
using System.Windows.Media;

namespace DiShare.Wpf.Converters
{
  public class IntToSolidColorBrushConverter : MarkupConverterBase
  {
    [ConstructorArgument("MoreThan20")]
    public SolidColorBrush MoreThan20 { get; set; }

    [ConstructorArgument("Between20And6")]
    public SolidColorBrush Between20And6 { get; set; }

    [ConstructorArgument("LessThan5")]
    public SolidColorBrush LessThan5 { get; set; }

    public IntToSolidColorBrushConverter()
    {
      this.MoreThan20 = new SolidColorBrush(Color.FromRgb((byte) 106, (byte) 168, (byte) 79));
      this.Between20And6 = new SolidColorBrush(Color.FromRgb((byte) 230, (byte) 194, (byte) 50));
      this.LessThan5 = new SolidColorBrush(Color.FromRgb(byte.MaxValue, (byte) 0, (byte) 0));
    }

    protected override object Convert(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (!(value is int num))
        return (object) null;
      if (num > 20)
        return (object) this.MoreThan20;
      return num <= 5 ? (object) this.LessThan5 : (object) this.Between20And6;
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
