

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
  public sealed class CannotApplyEqualityOperatorAttribute : Attribute
  {
  }
}
