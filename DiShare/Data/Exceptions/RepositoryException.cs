﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Data.Exceptions.RepositoryException
// Assembly: DiShare.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2D35AAB9-650B-4F3E-B05F-85D0483FCD61
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Data.dll

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Data.Exceptions
{
  public class RepositoryException : LibraryException
  {
    public RepositoryException(string message)
      : base(message)
    {
    }

    public RepositoryException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}