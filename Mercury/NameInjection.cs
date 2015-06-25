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
            return paramsInjected.Replace(Prefix + id, d.ToString());
        }

        private static string InjectGivenPrefix(string prefix, string str, object d)
        {
            if (!str.Contains(prefix)) return str;
            Type t = d.GetType();
            foreach (var p in t.GetProperties().OrderByDescending(p => p.Name))
            {
                if (p.GetIndexParameters().Any()) continue;
                str = str.Replace(prefix + p.Name, p.GetValue(d, null).ToString());
            }
            return str;
        }
    }
}