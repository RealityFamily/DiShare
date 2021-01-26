
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
  public sealed class AspMvcEditorTemplateAttribute : Attribute
  {
  }
}
