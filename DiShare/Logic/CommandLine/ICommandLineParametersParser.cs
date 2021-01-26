

using System.Collections.Generic;

namespace DiShare.Logic.CommandLine
{
  public interface ICommandLineParametersParser
  {
    void Parse(IReadOnlyCollection<string> args);
  }
}
