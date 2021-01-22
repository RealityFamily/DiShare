// Decompiled with JetBrains decompiler
// Type: DiShare.Infrastructure.TryGetResult
// Assembly: DiShare.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\DiShare.Infrastructure.dll

namespace DiShare.Infrastructure
{
  public static class TryGetResult
  {
    public static TryGetResult<T> NotFound<T>() => new TryGetResult<T>(false, default (T));

    public static TryGetResult<T> Found<T>(T result) => new TryGetResult<T>(true, result);
  }
}
