

using DiShare.Infrastructure.Exceptions;
using System;

namespace DiShare.Logic.Exceptions
{
  public class ScriptGeneratorException : LibraryException
  {
    public ScriptGeneratorException(string message)
      : base(message)
    {
    }

    public ScriptGeneratorException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
