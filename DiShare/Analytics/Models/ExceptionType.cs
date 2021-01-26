using System.ComponentModel;

namespace DiShare.Analytics.Models
{
  public enum ExceptionType
  {
    [Description("fatal")] Fatal,
    [Description("handled")] Handled,
    [Description("user")] User,
  }
}
