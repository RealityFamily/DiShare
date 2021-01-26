using DiShare.Analytics.Models;
using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using DiShare.Infrastructure.Threading;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DiShare.Analytics
{
  public class ExceptionTracker : IExceptionTracker
  {
    private readonly IAnalyticsTracker analyticsTracker;
    private AsyncLock asyncLock;

    public bool IsLogging { get; set; }

    public ExceptionTracker(IAnalyticsTracker analyticsTracker)
    {
      this.analyticsTracker = analyticsTracker;
      this.asyncLock = new AsyncLock();
    }

    public Task TrackAsync(
      Exception exception,
      ExceptionType exceptionType,
      bool includeStackTrace = true)
    {
      if (this.IsLogging)
      {
        using (this.asyncLock.LockAsync())
        {
          using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "3DHamster", "3DHamster.log"), true))
          {
            streamWriter.WriteLine(string.Format("{0} {1} {2}\nStackTrace: {3}", (object) DateTime.UtcNow, (object) exceptionType, (object) exceptionType.GetCachedDescription<ExceptionType>(), (object) exception.ExpandInnerExceptions()));
            streamWriter.Flush();
          }
        }
      }
      return (Task) this.analyticsTracker.SendEventAsync(nameof (exception), exceptionType.GetCachedDescription<ExceptionType>(), exception.ExpandInnerExceptions(includeStackTrace));
    }

    public void Track(Exception exception, ExceptionType exceptionType, bool includeStackTrace = true) => Task.Run((Func<Task>) (() => this.TrackAsync(exception, exceptionType, includeStackTrace)));
  }
}
