// Decompiled with JetBrains decompiler
// Type: Logic.Exceptions.UnauthorizedUserProviderException
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class UnauthorizedUserProviderException : LibraryException
  {
    public UnauthorizedUserProviderException(string message)
      : base(message)
    {
    }

    public UnauthorizedUserProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
