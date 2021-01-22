// Decompiled with JetBrains decompiler
// Type: DiShare.Wpf.Utils.ISingleInstanceApp
// Assembly: DiShare.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A2BD71AB-7428-4A2D-9534-22BB7FF61FE6
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Wpf.dll

using System.Collections.Generic;

namespace DiShare.Wpf.Utils
{
  public interface ISingleInstanceApp
  {
    bool SignalExternalCommandLineArgs(IList<string> args);
  }
}
