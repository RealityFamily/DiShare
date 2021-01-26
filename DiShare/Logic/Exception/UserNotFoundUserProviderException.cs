

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class UserNotFoundUserProviderException : LibraryException
  {
    public UserNotFoundUserProviderException(string message)
      : base(message)
    {
    }

    public UserNotFoundUserProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
