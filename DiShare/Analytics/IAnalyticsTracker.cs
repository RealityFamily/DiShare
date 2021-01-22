// Decompiled with JetBrains decompiler
// Type: DiShare.Analytics.IAnalyticsTracker
// Assembly: DiShare.Analytics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 074377AB-5F2B-4ED6-AC1C-A43B51B8190A
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Analytics.dll

using DiShare.Infrastructure;
using System.Threading.Tasks;

namespace DiShare.Analytics
{
  public interface IAnalyticsTracker
  {
    Task<TryResult> SendEventAsync(
      string category,
      string action,
      string label = null,
      string value = null);

    void SendEvent(string category, string action, string label = null, string value = null);
  }
}
