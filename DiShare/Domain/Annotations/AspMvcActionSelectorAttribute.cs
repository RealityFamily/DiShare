
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
  public sealed class AspMvcActionSelectorAttribute : Attribute
  {
  }
}
