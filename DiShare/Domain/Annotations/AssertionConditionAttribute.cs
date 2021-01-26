

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class AssertionConditionAttribute : Attribute
  {
    public AssertionConditionAttribute(AssertionConditionType conditionType) => this.ConditionType = conditionType;

    public AssertionConditionType ConditionType { get; }
  }
}
