using System;
using System.Text;

namespace DiShare.Infrastructure.Extensions
{
  public static class ExceptionExtensions
  {
    public static string ExpandInnerExceptions(this Exception exception, bool includeStackTrace = true)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine(exception.Message);
      for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
      {
        stringBuilder.Append(" > ");
        stringBuilder.AppendLine(innerException.Message);
      }
      if (includeStackTrace && !exception.StackTrace.IsNullOrWhiteSpace())
        stringBuilder.AppendLine(exception.StackTrace);
      return stringBuilder.ToString().TrimEnd('\n', '\r');
    }
  }
}
