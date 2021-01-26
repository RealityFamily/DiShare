
using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class UserProviderException : LibraryException
  {
    public UserProviderException(string message)
      : base(message)
    {
    }

    public UserProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
