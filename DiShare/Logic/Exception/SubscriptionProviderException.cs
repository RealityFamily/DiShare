// Decompiled with JetBrains decompiler
// Type: Logic.Exceptions.SubscriptionProviderException
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class SubscriptionProviderException : LibraryException
  {
    public SubscriptionProviderException(string message)
      : base(message)
    {
    }

    public SubscriptionProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
