// Decompiled with JetBrains decompiler
// Type: DiShare.Logic.Exceptions.CategoriesServiceException
// Assembly: DiShare.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CA4DCED3-3E6C-408B-8B3E-AE7715592923
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Services.dll

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class CategoriesServiceException : LibraryException
  {
    public CategoriesServiceException(string message)
      : base(message)
    {
    }

    public CategoriesServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
