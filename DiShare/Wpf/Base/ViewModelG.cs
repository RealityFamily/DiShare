// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Base.ViewModel`1
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

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
