

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Data.Exceptions
{
  public class ItemDetectorException : LibraryException
  {
    public ItemDetectorException(string message)
      : base(message)
    {
    }

    public ItemDetectorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
