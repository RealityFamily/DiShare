// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.CachedEnumDescription
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;

namespace Library.Infrastructure
{
  public static class CachedEnumDescription
  {
    public static string GetCachedDescription<T>(this T enumValue) where T : struct, IConvertible => CachedEnumDescription<T>.Get(enumValue);
  }
}
