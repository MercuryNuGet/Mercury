using System;
using System.Linq;

namespace Mercury
{
    internal static class NameInjection
    {
        private static readonly string Prefix = "#";

        internal static string Inject(string str, object d)
        {
            if (!str.Contains(Prefix)) return str;
            Type t = d.GetType();
            foreach (var p in t.GetProperties().OrderByDescending(p => p.Name))
            {
                if (p.GetIndexParameters().Any()) continue;
                str = str.Replace(Prefix + p.Name, p.GetValue(d, null).ToString());
            }
            return str;
        }
    }
}