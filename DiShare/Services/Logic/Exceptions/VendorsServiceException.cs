

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
