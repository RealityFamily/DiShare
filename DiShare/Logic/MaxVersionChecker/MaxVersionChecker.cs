

using System.Text.RegularExpressions;

namespace DiShare.Logic.MaxVersionChecker
{
  public class MaxVersionChecker : IMaxVersionChecker
  {
    private const string VersionCheckPattern = "^(201[0-3] - (32|64)bit)|((201[4-9]|202[0-9]) - 64bit)$";

    public bool IsSupportedVersion(string version) => Regex.Match(version, "^(201[0-3] - (32|64)bit)|((201[4-9]|202[0-9]) - 64bit)$").Success;
  }
}
