using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DiShare.Infrastructure.OperationSystem
{
  [ExcludeFromCodeCoverage]
  public class ProcessRunner : IProcessRunner
  {
    public void Start(ProcessStartInfo startInfo) => Process.Start(startInfo);
  }
}
