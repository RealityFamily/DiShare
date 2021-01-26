
using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Data.Exceptions
{
  public class CacheException : LibraryException
  {
    public CacheException(string message)
      : base(message)
    {
    }

    public CacheException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
