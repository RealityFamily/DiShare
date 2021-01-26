

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
  public sealed class AspDataFieldsAttribute : Attribute
  {
  }
}
