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
