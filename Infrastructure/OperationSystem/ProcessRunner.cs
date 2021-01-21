// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.OperationSystem.ProcessRunner
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Library.Infrastructure.OperationSystem
{
  [ExcludeFromCodeCoverage]
  public class ProcessRunner : IProcessRunner
  {
    public void Start(ProcessStartInfo startInfo) => Process.Start(startInfo);
  }
}
