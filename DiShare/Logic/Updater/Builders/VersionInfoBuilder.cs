﻿

using DiShare.Infrastructure.Extensions;
using DiShare.Logic.Updater.Models;
using System;

namespace DiShare.Logic.Updater.Builders
{
  public class VersionInfoBuilder : IVersionInfoBuilder
  {
    public VersionInfo Build(string version)
    {
      string[] strArray = !version.IsNullOrWhiteSpace() ? version.Split('.') : throw new ArgumentException("Version string is null or empty");
      string s1 = strArray.Length != 0 ? strArray[0] : "0";
      string s2 = strArray.Length > 1 ? strArray[1] : "0";
      string s3 = strArray.Length > 2 ? strArray[2] : "0";
      string s4 = strArray.Length > 3 ? strArray[3] : "0";
      return new VersionInfo(int.Parse(s1), int.Parse(s2), int.Parse(s3), int.Parse(s4));
    }
  }
}
