

using DiShare.Domain.Models;
using DiShare.Infrastructure;

namespace DiShare.Logic.ScriptGenerators
{
  public interface IScriptGenerator
  {
    TryResult<string> Generate(ModelItem item);
  }
}
