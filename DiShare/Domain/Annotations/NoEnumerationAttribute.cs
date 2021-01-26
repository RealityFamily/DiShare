
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Parameter)]
  public sealed class NoEnumerationAttribute : Attribute
  {
  }
}
