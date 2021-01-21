// Decompiled with JetBrains decompiler
// Type: Logic.ShellExecutor.IShellExecutor
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

namespace DiShare.Logic.ShellExecutor
{
  public interface IShellExecutor
  {
    void Execute(string uri);

    void Execute(string filename, string arguments);
  }
}
