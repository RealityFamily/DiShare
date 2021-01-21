// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.TryResult
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;

namespace Library.Infrastructure
{
  public struct TryResult
  {
    public TryResult(Exception e) => this.Exception = e ?? throw new ArgumentNullException(nameof (e));

    public bool IsFaulted => this.Exception != null;

    public Exception Exception { get; }

    public override string ToString() => !this.IsFaulted ? "void" : this.Exception.ToString();
  }
}
