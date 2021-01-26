

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DiShare.Wpf.Converters
{
  [MarkupExtensionReturnType(typeof (IMultiValueConverter))]
  public abstract class MarkupMultiValueConverterBase : MarkupExtension, IMultiValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider) => (object) this;

    protected abstract object Convert(
      object[] values,
      Type targetType,
      object parameter,
      CultureInfo culture);

    protected abstract object[] ConvertBack(
      object value,
      Type[] targetTypes,
      object parameter,
      CultureInfo culture);

    object IMultiValueConverter.Convert(
      object[] values,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      try
      {
        return this.Convert(values, targetType, parameter, culture);
      }
      catch
      {
        return DependencyProperty.UnsetValue;
      }
    }

    object[] IMultiValueConverter.ConvertBack(
      object value,
      Type[] targetTypes,
      object parameter,
      CultureInfo culture)
    {
      try
      {
        return this.ConvertBack(value, targetTypes, parameter, culture);
      }
      catch
      {
        return (object[]) null;
      }
    }
  }
}
