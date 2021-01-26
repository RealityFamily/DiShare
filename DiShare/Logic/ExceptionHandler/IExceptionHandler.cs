

using DiShare.Analytics.Models;
using DiShare.Logic.ErrorHandler.Models;
using System;

namespace DiShare.Logic.ExceptionHandler
{
  public interface IExceptionHandler
  {
    void HandleException(
      Exception ex,
      ExceptionType exceptionType = ExceptionType.Handled,
      bool includeStackTrace = true,
      NotificationMode notificationMode = NotificationMode.Notify,
      TrackingMode trackingMode = TrackingMode.Enabled);
  }
}
