using System;
using System.Linq;

namespace Mercury
{
    internal static class NameInjection
    {
        internal static string Inject(string str, object d)
        {
            Type t = d.GetType();
            foreach (var p in t.GetProperties().OrderByDescending(p => p.Name))
            {
                str = str.Replace("#" + p.Name, p.GetValue(d, null).ToString());
            }
            return str;
        }
    }
}