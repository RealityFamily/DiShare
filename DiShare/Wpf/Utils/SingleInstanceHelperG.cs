// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Utils.SingleInstanceHelper`1
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System;
using System.Windows;

namespace DiShare.Wpf.Utils
{
  public static class SingleInstanceHelper<TApplication> where TApplication : Application, ISingleInstanceApp
  {
    public static void TryExecute(string mutex, Action action)
    {
      if (!SingleInstance<TApplication>.InitializeAsFirstInstance(mutex))
        return;
      action();
      SingleInstance<TApplication>.Cleanup();
    }
  }
}
