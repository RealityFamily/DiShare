﻿// Decompiled with JetBrains decompiler
// Type: Logic.CryptHelper.CryptHelper
// Assembly: Library.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

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
