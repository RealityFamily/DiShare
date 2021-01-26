using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.All)]
  public sealed class LocalizationRequiredAttribute : Attribute
  {
    public LocalizationRequiredAttribute()
      : this(true)
    {
    }

    public LocalizationRequiredAttribute(bool required) => this.Required = required;

    public bool Required { get; }
  }
}
