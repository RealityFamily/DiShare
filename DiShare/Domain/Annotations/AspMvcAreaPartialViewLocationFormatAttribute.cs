

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
  {
    public AspMvcAreaPartialViewLocationFormatAttribute([NotNull] string format) => this.Format = format;

    [NotNull]
    public string Format { get; }
  }
}
