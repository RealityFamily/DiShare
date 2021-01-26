

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
