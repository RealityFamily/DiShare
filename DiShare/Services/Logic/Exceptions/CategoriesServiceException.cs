

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class CategoriesServiceException : LibraryException
  {
    public CategoriesServiceException(string message)
      : base(message)
    {
    }

    public CategoriesServiceException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
