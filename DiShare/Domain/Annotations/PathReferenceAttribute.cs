// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.PathReferenceAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class PathReferenceAttribute : Attribute
  {
    public PathReferenceAttribute()
    {
    }

    public PathReferenceAttribute([NotNull, PathReference] string basePath) => this.BasePath = basePath;

    [CanBeNull]
    public string BasePath { get; }
  }
}
