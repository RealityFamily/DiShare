// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.MustUseReturnValueAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class MustUseReturnValueAttribute : Attribute
  {
    public MustUseReturnValueAttribute()
    {
    }

    public MustUseReturnValueAttribute([NotNull] string justification) => this.Justification = justification;

    [CanBeNull]
    public string Justification { get; }
  }
}
