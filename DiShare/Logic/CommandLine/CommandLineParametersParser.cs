

using DiShare.Infrastructure.Extensions;
using DiShare.Logic.LocalParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiShare.Logic.CommandLine
{
  public class CommandLineParametersParser : ICommandLineParametersParser
  {
    private readonly ILocalParameters _localParameters;

    public CommandLineParametersParser(ILocalParameters localParameters) => this._localParameters = localParameters;

    public void Parse(IReadOnlyCollection<string> args)
    {
      if (args == null || args.Count == 0 || !CommandLineParametersParser.ContainsCommand(args, CommandTokens.UpdateInternalPack))
        return;
      this._localParameters.SetNeedUpdateInternalPack();
    }

    private static bool ContainsCommand(IReadOnlyCollection<string> args, string command) => args.Any<string>((Func<string, bool>) (s => s.IsEqualsIgnoreCase("/" + command) || s.IsEqualsIgnoreCase("-" + command) || s.IsEqualsIgnoreCase("--" + command)));
  }
}
