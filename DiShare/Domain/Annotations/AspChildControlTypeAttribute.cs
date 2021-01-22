// Decompiled with JetBrains decompiler
// Type: DiShare.Domain.Annotations.AspChildControlTypeAttribute
// Assembly: DiShare.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8ED9EA1E-6284-4CC7-A45B-49528D453FED
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Domain.dll

using System;

namespace DiShare.Domain.Annotations
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public sealed class AspChildControlTypeAttribute : Attribute
  {
    public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
    {
      this.TagName = tagName;
      this.ControlType = controlType;
    }

    [NotNull]
    public string TagName { get; }

    [NotNull]
    public Type ControlType { get; }
  }
}
