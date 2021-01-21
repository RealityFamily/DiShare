// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.Exceptions.LibraryException
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;

namespace DiShare.Infrastructure.Exceptions
{
  public class LibraryException : Exception
  {
    public LibraryException(string message)
      : base(message)
    {
    }

    public LibraryException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
