// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.StringFormatMethodAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
  public sealed class StringFormatMethodAttribute : Attribute
  {
    public StringFormatMethodAttribute([NotNull] string formatParameterName) => this.FormatParameterName = formatParameterName;

    [NotNull]
    public string FormatParameterName { get; }
  }
}
