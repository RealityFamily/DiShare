

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
  public sealed class AspMvcSuppressViewErrorAttribute : Attribute
  {
  }
}
