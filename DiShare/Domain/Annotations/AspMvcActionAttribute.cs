// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.AspMvcActionAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
  public sealed class AspMvcActionAttribute : Attribute
  {
    public AspMvcActionAttribute()
    {
    }

    public AspMvcActionAttribute([NotNull] string anonymousProperty) => this.AnonymousProperty = anonymousProperty;

    [CanBeNull]
    public string AnonymousProperty { get; }
  }
}
