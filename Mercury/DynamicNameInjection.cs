using System;
using System.Linq;

namespace Mercury
{
    public static class DynamicNameInjection
    {
        public static string Inject(string str, dynamic d)
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