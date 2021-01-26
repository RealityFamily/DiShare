

using System.Diagnostics;

namespace DiShare.Infrastructure.OperationSystem
{
  public interface IProcessRunner
  {
    void Start(ProcessStartInfo startInfo);
  }
}
