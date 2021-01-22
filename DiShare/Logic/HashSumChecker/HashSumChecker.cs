// Decompiled with JetBrains decompiler
// Type: Logic.HashSumChecker.HashSumChecker
// Assembly: DiShare.Logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DE47CAB1-D2AE-4243-A344-116EBB0A3A61
// Assembly location: W:\Program Files (x86)\3D Hamster\Logic.dll

using DiShare.Infrastructure;
using DiShare.Infrastructure.Extensions;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DiShare.Logic.HashSumChecker
{
  public class HashSumChecker : IHashSumChecker
  {
    public TryResult<bool> CheckFile(string file, string hash)
    {
      try
      {
        if (!File.Exists(file))
          throw new FileNotFoundException(file);
        using (MD5 md5 = MD5.Create())
        {
          using (FileStream fileStream = File.OpenRead(file))
            return (TryResult<bool>) BitConverter.ToString(md5.ComputeHash((Stream) fileStream)).Replace("-", "").ToLowerInvariant().IsEqualsIgnoreCase(hash);
        }
      }
      catch (Exception ex)
      {
        return new TryResult<bool>(ex);
      }
    }
  }
}
