﻿// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.RazorPageBaseTypeAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public sealed class RazorPageBaseTypeAttribute : Attribute
  {
    public RazorPageBaseTypeAttribute([NotNull] string baseType) => this.BaseType = baseType;

    public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
    {
      this.BaseType = baseType;
      this.PageName = pageName;
    }

    [NotNull]
    public string BaseType { get; }

    [CanBeNull]
    public string PageName { get; }
  }
}