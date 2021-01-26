
using System.Collections.Generic;

namespace DiShare.Wpf.Utils
{
  public interface ISingleInstanceApp
  {
    bool SignalExternalCommandLineArgs(IList<string> args);
  }
}
