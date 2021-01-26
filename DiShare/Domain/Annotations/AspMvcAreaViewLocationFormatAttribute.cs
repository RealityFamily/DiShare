

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
  public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
  {
    public AspMvcAreaViewLocationFormatAttribute([NotNull] string format) => this.Format = format;

    [NotNull]
    public string Format { get; }
  }
}
