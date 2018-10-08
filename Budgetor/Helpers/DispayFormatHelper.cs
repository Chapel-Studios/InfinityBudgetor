using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Helpers
{
    public static class DispayFormatHelper
    {
        public static string GetDisplayAmountText(decimal value)
        {
            return string.Format(value.ToString(), "money");
        }

        public static string GetDisplayDate(DateTime value)
        {
            return string.Format(value.ToString(), "d/m/yy");
        }

        public static string GetDisplayDate(DateTime? value)
        {
            if (value.HasValue)
            {
                return GetDisplayDate(value.Value);
            }
            else
            {
                return "";
            }
        }

    }
}
