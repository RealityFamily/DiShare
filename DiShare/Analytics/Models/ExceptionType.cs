﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Analytics.Models.ExceptionType
// Assembly: DiShare.Analytics, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 074377AB-5F2B-4ED6-AC1C-A43B51B8190A
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Analytics.dll

using System.ComponentModel;

namespace DiShare.Analytics.Models
{
  public enum ExceptionType
  {
    [Description("fatal")] Fatal,
    [Description("handled")] Handled,
    [Description("user")] User,
  }
}