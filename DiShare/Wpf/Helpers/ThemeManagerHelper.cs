

using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using ControlzEx.Theming;

namespace DiShare.Wpf.Helpers
{
  public class ThemeManagerHelper
  {
    public static void CreateAppStyleBy(Color color, bool changeImmediately = false)
    {
      ResourceDictionary resourceDictionary1 = new ResourceDictionary()
      {
        {
          (object) "HighlightColor",
          (object) color
        },
        {
          (object) "AccentBaseColor",
          (object) color
        },
        {
          (object) "AccentColor",
          (object) Color.FromArgb((byte) 204, color.R, color.G, color.B)
        },
        {
          (object) "AccentColor2",
          (object) Color.FromArgb((byte) 153, color.R, color.G, color.B)
        },
        {
          (object) "AccentColor3",
          (object) Color.FromArgb((byte) 102, color.R, color.G, color.B)
        },
        {
          (object) "AccentColor4",
          (object) Color.FromArgb((byte) 51, color.R, color.G, color.B)
        }
      };
      resourceDictionary1.Add((object) "HighlightBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "HighlightColor"]));
      resourceDictionary1.Add((object) "AccentBaseColorBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentBaseColor"]));
      resourceDictionary1.Add((object) "AccentColorBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "AccentColorBrush2", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor2"]));
      resourceDictionary1.Add((object) "AccentColorBrush3", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor3"]));
      resourceDictionary1.Add((object) "AccentColorBrush4", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor4"]));
      resourceDictionary1.Add((object) "WindowTitleColorBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "ProgressBrush", (object) new LinearGradientBrush(new GradientStopCollection((IEnumerable<GradientStop>) new GradientStop[2]
      {
        new GradientStop((Color) resourceDictionary1[(object) "HighlightColor"], 0.0),
        new GradientStop((Color) resourceDictionary1[(object) "AccentColor3"], 1.0)
      }), new Point(1.002, 0.5), new Point(0.001, 0.5)));
      resourceDictionary1.Add((object) "CheckmarkFill", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "RightArrowFill", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "IdealForegroundColor", (object) ThemeManagerHelper.IdealTextColor(color));
      resourceDictionary1.Add((object) "IdealForegroundColorBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"]));
      resourceDictionary1.Add((object) "IdealForegroundDisabledBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"], 0.4));
      resourceDictionary1.Add((object) "AccentSelectedColorBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"]));
      resourceDictionary1.Add((object) "MetroDataGrid.HighlightBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "MetroDataGrid.HighlightTextBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"]));
      resourceDictionary1.Add((object) "MetroDataGrid.MouseOverHighlightBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor3"]));
      resourceDictionary1.Add((object) "MetroDataGrid.FocusBorderBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "MetroDataGrid.InactiveSelectionHighlightBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor2"]));
      resourceDictionary1.Add((object) "MetroDataGrid.InactiveSelectionHighlightTextBrush", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"]));
      resourceDictionary1.Add((object) "MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchBrush.Win10", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor"]));
      resourceDictionary1.Add((object) "MahApps.Metro.Brushes.ToggleSwitchButton.OnSwitchMouseOverBrush.Win10", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "AccentColor2"]));
      resourceDictionary1.Add((object) "MahApps.Metro.Brushes.ToggleSwitchButton.ThumbIndicatorCheckedBrush.Win10", (object) ThemeManagerHelper.GetSolidColorBrush((Color) resourceDictionary1[(object) "IdealForegroundColor"]));
      string path2 = string.Format("ApplicationAccent_{0}.xaml", (object) color.ToString().Replace("#", string.Empty));
      string uriString = Path.Combine(Path.GetTempPath(), path2);
      string outputFileName = uriString;
      using (XmlWriter xmlWriter = XmlWriter.Create(outputFileName, new XmlWriterSettings()
      {
        Indent = true
      }))
      {
        XamlWriter.Save((object) resourceDictionary1, xmlWriter);
        xmlWriter.Close();
      }
      ResourceDictionary resourceDictionary2 = new ResourceDictionary()
      {
        Source = new Uri(uriString, UriKind.Absolute)
      };
      /*Accent newAccent = new Accent()
      {
        Name = path2,
        Resources = resourceDictionary2
      };
      ThemeManager.AddAccent(newAccent.Name, newAccent.Resources.Source);
      if (!changeImmediately)
        return;
      Application current = Application.Current;
      Tuple<AppTheme, Accent> tuple = ThemeManager.DetectAppStyle(current);
      ThemeManager.ChangeAppStyle(current, newAccent, tuple.Item1);*/
    }

    private static Color IdealTextColor(Color color) => (int) byte.MaxValue - Convert.ToInt32((double) color.R * 0.299 + (double) color.G * 0.587 + (double) color.B * 0.114) >= 105 ? Colors.White : Colors.Black;

    private static SolidColorBrush GetSolidColorBrush(Color color, double opacity = 1.0)
    {
      SolidColorBrush solidColorBrush = new SolidColorBrush(color);
      solidColorBrush.Opacity = opacity;
      solidColorBrush.Freeze();
      return solidColorBrush;
    }
  }
}
