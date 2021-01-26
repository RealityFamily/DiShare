
using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public sealed class RazorImportNamespaceAttribute : Attribute
  {
    public RazorImportNamespaceAttribute([NotNull] string name) => this.Name = name;

    [NotNull]
    public string Name { get; }
  }
}
