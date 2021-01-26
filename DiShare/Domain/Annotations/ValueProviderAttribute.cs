﻿

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
  public sealed class ValueProviderAttribute : Attribute
  {
    public ValueProviderAttribute([NotNull] string name) => this.Name = name;

    [NotNull]
    public string Name { get; }
  }
}
