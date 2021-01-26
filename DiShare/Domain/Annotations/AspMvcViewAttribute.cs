

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
  public sealed class AspMvcViewAttribute : Attribute
  {
  }
}
