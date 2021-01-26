﻿
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
