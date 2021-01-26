
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate)]
  public sealed class ItemNotNullAttribute : Attribute
  {
  }
}
