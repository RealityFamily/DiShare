using DiShare.Analytics.Models;
using System;
using System.Threading.Tasks;

namespace DiShare.Analytics
{
  public interface IExceptionTracker
  {
    bool IsLogging { get; set; }

    Task TrackAsync(Exception exception, ExceptionType exceptionType, bool includeStackTrace = true);

    void Track(Exception exception, ExceptionType exceptionType, bool includeStackTrace = true);
  }
}
