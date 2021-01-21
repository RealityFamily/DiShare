// Decompiled with JetBrains decompiler
// Type: Logic.ExceptionHandler.IExceptionHandler
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
