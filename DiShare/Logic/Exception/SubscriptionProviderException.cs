

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class SubscriptionProviderException : LibraryException
  {
    public SubscriptionProviderException(string message)
      : base(message)
    {
    }

    public SubscriptionProviderException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
