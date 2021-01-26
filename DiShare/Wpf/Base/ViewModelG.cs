

using System.Windows;

namespace DiShare.Wpf.Base
{
  public abstract class ViewModel<TView> : ViewModel where TView : FrameworkElement, new()
  {
    public TView TypedView => (TView) this.View;

    public override FrameworkElement View
    {
      get => base.View ?? (base.View = (FrameworkElement) new TView());
      set => base.View = value;
    }
  }
}
