

using System;

namespace DiShare.Domain.Annotations
{
  [Obsolete("Use [ContractAnnotation('=> halt')] instead")]
  [AttributeUsage(AttributeTargets.Method)]
  public sealed class TerminatesProgramAttribute : Attribute
  {
  }
}
