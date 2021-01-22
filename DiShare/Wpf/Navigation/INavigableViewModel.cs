// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Navigation.INavigableViewModel
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

namespace DiShare.Wpf.Navigation
{
  public interface INavigableViewModel
  {
    void OnNavigated(object extraData);

    void OnNavigatedBack(object extraData);
  }
}
