// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.Extensions.ExceptionExtensions
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;
using System.Text;

namespace Library.Infrastructure.Extensions
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
