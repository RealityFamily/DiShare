

using DiShare.Infrastructure;

namespace DiShare.Logic.HashSumChecker
{
  public interface IHashSumChecker
  {
    TryResult<bool> CheckFile(string file, string hash);
  }
}
