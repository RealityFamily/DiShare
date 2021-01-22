// Decompiled with JetBrains decompiler
// Type: Logic.Updater.Builders.IVersionInfoBuilder
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Logic.Updater.Models;

namespace DiShare.Logic.Updater.Builders
{
  public interface IVersionInfoBuilder
  {
    VersionInfo Build(string version);
  }
}
