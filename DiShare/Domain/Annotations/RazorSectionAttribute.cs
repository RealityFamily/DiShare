
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
  public sealed class RazorSectionAttribute : Attribute
  {
  }
}
