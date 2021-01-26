

using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace DiShare.Wpf.Behaviors
{
  public static class HyperlinkBehavior
  {
    public static readonly DependencyProperty IsExternalProperty = DependencyProperty.RegisterAttached("IsExternal", typeof (bool), typeof (HyperlinkBehavior), (PropertyMetadata) new UIPropertyMetadata((object) false, new PropertyChangedCallback(HyperlinkBehavior.OnIsExternalChanged)));

    public static bool GetIsExternal(DependencyObject obj) => (bool) obj.GetValue(HyperlinkBehavior.IsExternalProperty);

    public static void SetIsExternal(DependencyObject obj, bool value) => obj.SetValue(HyperlinkBehavior.IsExternalProperty, (object) value);

    private static void OnIsExternalChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
      if (!(sender is Hyperlink hyperlink))
        return;
      if ((bool) args.NewValue)
        hyperlink.RequestNavigate += new RequestNavigateEventHandler(HyperlinkBehavior.HyperlinkRequestNavigate);
      else
        hyperlink.RequestNavigate -= new RequestNavigateEventHandler(HyperlinkBehavior.HyperlinkRequestNavigate);
    }

    private static void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      try
      {
        Process.Start(new ProcessStartInfo(e.Uri.ToString()));
      }
      catch
      {
      }
      e.Handled = true;
    }
  }
}
