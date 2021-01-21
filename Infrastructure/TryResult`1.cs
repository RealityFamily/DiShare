// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.TryResult`1
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;

namespace Library.Infrastructure
{
  public struct TryResult<T>
  {
    private readonly T _value;
    private readonly Exception _exception;

    public TryResult(T value)
    {
      this._value = value;
      this._exception = (Exception) null;
    }

    public TryResult(Exception e)
    {
      this._exception = e ?? throw new ArgumentNullException(nameof (e));
      this._value = default (T);
    }

    public static implicit operator TryResult<T>(T value) => new TryResult<T>(value);

    public bool IsFaulted => this._exception != null;

    public T Value => this._value;

    public Exception Exception => this._exception;

    public T GetValueOrThrow()
    {
      if (this.IsFaulted)
        throw this.Exception;
      return this.Value;
    }

    public override string ToString() => !this.IsFaulted ? Convert.ToString((object) this._value) : this._exception.ToString();
  }
}
