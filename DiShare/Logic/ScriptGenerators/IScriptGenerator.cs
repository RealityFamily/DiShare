﻿// Decompiled with JetBrains decompiler
// Type: Logic.ScriptGenerators.IScriptGenerator
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Domain.Models;
using DiShare.Infrastructure;

namespace DiShare.Logic.ScriptGenerators
{
  public interface IScriptGenerator
  {
    TryResult<string> Generate(ModelItem item);
  }
}
