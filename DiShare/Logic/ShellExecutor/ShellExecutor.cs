

using DiShare.Infrastructure.OperationSystem;
using DiShare.Logic.ExceptionHandler;
using System;
using System.Diagnostics;

namespace DiShare.Logic.ShellExecutor
{
  public class ShellExecutor : IShellExecutor
  {
    private readonly IProcessRunner processRunner;
    private readonly IExceptionHandler exceptionHandler;

    public ShellExecutor(IProcessRunner processRunner, IExceptionHandler exceptionHandler)
    {
      this.processRunner = processRunner;
      this.exceptionHandler = exceptionHandler;
    }

    public void Execute(string uri)
    {
      try
      {
        this.processRunner.Start(new ProcessStartInfo(uri));
      }
      catch (Exception ex)
      {
        this.exceptionHandler.HandleException(ex);
      }
    }

    public void Execute(string filename, string arguments)
    {
      try
      {
        this.processRunner.Start(new ProcessStartInfo()
        {
          FileName = filename,
          Arguments = arguments
        });
      }
      catch (Exception ex)
      {
        this.exceptionHandler.HandleException(ex);
      }
    }
  }
}
