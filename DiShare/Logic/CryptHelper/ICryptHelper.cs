

namespace DiShare.Logic.CryptHelper
{
  public interface ICryptHelper
  {
    byte[] HashString(string input);

    string ArrayToString(byte[] array);
  }
}
