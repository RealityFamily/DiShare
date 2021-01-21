// Decompiled with JetBrains decompiler
// Type: Library.Infrastructure.Extensions.StringExtensions
// Assembly: Library.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C33DEAF4-0DED-4856-9D39-1C1FEE34CC77
// Assembly location: W:\Program Files (x86)\3D Hamster\Library.Infrastructure.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Extensions
{
  public static class StringExtensions
  {
    private const char DotChar = '.';
    private const char SpaceChar = ' ';
    private static readonly HashSet<char> LineEndCharacters = new HashSet<char>()
    {
      '.',
      ',',
      ';',
      ':',
      '-',
      '!',
      '?'
    };

    public static string ToSingleLine(this string source)
    {
      if (string.IsNullOrEmpty(source))
        return (string) null;
      StringBuilder stringBuilder = new StringBuilder();
      bool flag1 = true;
      bool flag2 = false;
      foreach (char ch in source)
      {
        if (!object.Equals((object) ch, (object) '\r') && !object.Equals((object) ch, (object) '\n'))
        {
          stringBuilder.Append(ch);
          flag1 = StringExtensions.LineEndCharacters.Contains(ch);
          flag2 = false;
        }
        else if (flag1)
        {
          if (!flag2)
          {
            stringBuilder.Append(' ');
            flag2 = true;
          }
        }
        else
        {
          stringBuilder.Append('.');
          stringBuilder.Append(' ');
          flag1 = true;
          flag2 = true;
        }
      }
      return stringBuilder.ToString().Trim();
    }

    public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

    public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

    public static bool IsEqualsIgnoreCase(this string source, string searchString) => !source.IsNullOrWhiteSpace() && source.Equals(searchString, StringComparison.OrdinalIgnoreCase);

    public static bool IsMatchingIgnoreCase(this string str, string searchString) => !string.IsNullOrWhiteSpace(str) && str.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0;
  }
}
