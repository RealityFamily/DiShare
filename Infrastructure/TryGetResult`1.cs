// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.TryGetResult`1
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

namespace Library.Infrastructure
{
  public struct TryGetResult<T>
  {
    public TryGetResult(bool isFound, T value)
    {
      this.IsFound = isFound;
      this.Value = value;
    }

    public bool IsFound { get; }

    public T Value { get; }
  }
}
