// Decompiled with JetBrains decompiler
// Type: DiShare.Analytics.IExceptionTracker
// Assembly: DiShare.Analytics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 074377AB-5F2B-4ED6-AC1C-A43B51B8190A
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Analytics.dll

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
