

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
