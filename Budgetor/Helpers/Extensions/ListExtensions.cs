using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Helpers.Extensions
{
    internal static class ListExtensions
    {
        public static List<int> DeriveIntList(this string stringList, char separator)
        {
            List<int> r = new List<int>();
            List<string> l = stringList.Split(separator).ToList();
            foreach (string s in l)
            {
                bool b = int.TryParse(s, out int i);
                if (b) r.Add(i);
            }
            return r;
        }
    }
}
