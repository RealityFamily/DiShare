

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
