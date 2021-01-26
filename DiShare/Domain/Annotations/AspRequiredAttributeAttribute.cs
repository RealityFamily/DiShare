

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class AspRequiredAttributeAttribute : System.Attribute
  {
    public AspRequiredAttributeAttribute([NotNull] string attribute) => this.Attribute = attribute;

    [NotNull]
    public string Attribute { get; }
  }
}
