using System;
using System.Linq;

namespace Mercury
{
    internal static class NameInjection
    {
        private const string Prefix = "#";

        internal static string Inject(string str, object d)
        {
            return InjectGivenPrefix(Prefix, str, d);
        }

        public static string Inject(string id, string str, object d)
        {
            var paramsInjected = InjectGivenPrefix(Prefix + id + ".", str, d);
            return paramsInjected.Replace(Prefix + id, GetStringForValue(d));
        }
        private static string InjectGivenPrefix(string prefix, string str, object d)
        {
            if (!str.Contains(prefix)) return str;
            if (d == null) return str;
            Type t = d.GetType();
            foreach (var p in t.GetProperties().OrderByDescending(p => p.Name))
            {
                if (p.GetIndexParameters().Any()) continue;
                str = str.Replace(prefix + p.Name, GetStringForValue(p.GetValue(d, null)));
            }
            return str;
        }

        private static string GetStringForValue(object d)
        {
            return d == null ? "null" : d.ToString();
        }

    }
}