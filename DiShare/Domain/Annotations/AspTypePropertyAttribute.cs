

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class AspTypePropertyAttribute : Attribute
  {
    public bool CreateConstructorReferences { get; }

    public AspTypePropertyAttribute(bool createConstructorReferences) => this.CreateConstructorReferences = createConstructorReferences;
  }
}
