// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.PublicAPIAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
  [AttributeUsage(AttributeTargets.All, Inherited = false)]
  public sealed class PublicAPIAttribute : Attribute
  {
    public PublicAPIAttribute()
    {
    }

    public PublicAPIAttribute([NotNull] string comment) => this.Comment = comment;

    [CanBeNull]
    public string Comment { get; }
  }
}
