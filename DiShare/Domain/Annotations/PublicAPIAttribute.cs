
using System;

namespace DiShare.Domain.Annotations
{
  [MeansImplicitUse(ImplicitUseTargetFlags.WithMembers)]
  [AttributeUsage(AttributeTargets.All, Inherited = false)]
  public sealed class PublicAPIAttribute : Attribute
  {
    public PublicAPIAttribute()
    {
    }

    public PublicAPIAttribute([NotNull] string comment) => this.Comment = comment;

    [CanBeNull]
    public string Comment { get; }
  }
}
