

using System.Security.Cryptography;
using System.Text;

namespace DiShare.Logic.CryptHelper
{
  public class CryptHelper : ICryptHelper
  {
    public byte[] HashString(string input) => MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(input));

    public string ArrayToString(byte[] array)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < array.Length; ++index)
        stringBuilder.Append(array[index].ToString("X2"));
      return stringBuilder.ToString();
    }
  }
}
