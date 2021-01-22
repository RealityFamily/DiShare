// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Navigation.NavigablePage
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System.Windows;
using System.Windows.Controls;

namespace DiShare.Wpf.Navigation
{
  public class NavigablePage : Page
  {
    public static DependencyProperty ExtraDataProperty = DependencyProperty.Register(nameof (ExtraData), typeof (object), typeof (NavigablePage));

    public object ExtraData
    {
      get => this.GetValue(NavigablePage.ExtraDataProperty);
      set => this.SetValue(NavigablePage.ExtraDataProperty, value);
    }

    public virtual void OnNavigated(object extraData)
    {
      this.ExtraData = extraData;
      if (this.DataContext == null || !(this.DataContext is INavigableViewModel dataContext))
        return;
      dataContext.OnNavigated(extraData);
    }

    public virtual void OnNavigatedBack(object extraData)
    {
      this.ExtraData = extraData;
      if (this.DataContext == null || !(this.DataContext is INavigableViewModel dataContext))
        return;
      dataContext.OnNavigatedBack(extraData);
    }
  }
}
