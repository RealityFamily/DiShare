

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Data.Exceptions
{
  public class ProviderException : LibraryException
  {
    public ProviderException(string message)
      : base(message)
    {
    }

    public ProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
