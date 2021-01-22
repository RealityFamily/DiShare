// Decompiled with JetBrains decompiler
// Type: DiShare.Infrastructure.OperationSystem.IProcessRunner
// Assembly: DiShare.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Infrastructure.dll

using System.Diagnostics;

namespace DiShare.Infrastructure.OperationSystem
{
  public interface IProcessRunner
  {
    void Start(ProcessStartInfo startInfo);
  }
}
