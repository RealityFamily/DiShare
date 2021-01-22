// Decompiled with JetBrains decompiler
// Type: DiShare.Logic.Exceptions.VendorsServiceException
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class VendorsServiceException : LibraryException
  {
    public VendorsServiceException(string message)
      : base(message)
    {
    }

    public VendorsServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
