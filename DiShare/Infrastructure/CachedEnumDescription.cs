using System;

namespace DiShare.Infrastructure
{
    public static class CachedEnumDescription
    {
        public static string GetCachedDescription<T>(this T enumValue) where T : struct, IConvertible => CachedEnumDescription<T>.Get(enumValue);
    }
}