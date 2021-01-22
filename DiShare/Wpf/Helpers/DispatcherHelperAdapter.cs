// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Helpers.DispatcherHelperAdapter
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using GalaSoft.MvvmLight.Threading;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;

namespace DiShare.Wpf.Helpers
{
  [ExcludeFromCodeCoverage]
  public class DispatcherHelperAdapter : IDispatcherHelper
  {
    public DispatcherHelperAdapter() => DispatcherHelper.Initialize();

    public Dispatcher UIDispatcher => DispatcherHelper.UIDispatcher;

    public void CheckBeginInvokeOnUI(Action action) => DispatcherHelper.CheckBeginInvokeOnUI(action);

    public DispatcherOperation RunAsync(Action action) => DispatcherHelper.RunAsync(action);

    public void Reset() => DispatcherHelper.Reset();
  }
}
