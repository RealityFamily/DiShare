using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiShare.Infrastructure
{
    public static class CachedEnumDescription<T> where T : struct, IConvertible
    {
        private static readonly Dictionary<T, string> CachedDescriptions;

        static CachedEnumDescription()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");
            CachedEnumDescription<T>.CachedDescriptions = new Dictionary<T, string>((IDictionary<T, string>)Enum.GetValues(typeof(T)).Cast<T>().ToDictionary<T, T, string>((Func<T, T>)(smiType => smiType), new Func<T, string>(CachedEnumDescription<T>.GetDescription)));
        }

        public static IReadOnlyDictionary<T, string> GetAll() => (IReadOnlyDictionary<T, string>)CachedEnumDescription<T>.CachedDescriptions;

        public static string Get(T enumValue)
        {
            string str;
            CachedEnumDescription<T>.CachedDescriptions.TryGetValue(enumValue, out str);
            return str;
        }

        private static string GetDescription(T enumValue)
        {
            string description = enumValue.ToString((IFormatProvider)CultureInfo.InvariantCulture);
            object[] customAttributes = enumValue.GetType().GetField(enumValue.ToString((IFormatProvider)CultureInfo.InvariantCulture)).GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (customAttributes.Length != 0)
                description = ((DescriptionAttribute)customAttributes[0]).Description;
            return description;
        }
    }
}
