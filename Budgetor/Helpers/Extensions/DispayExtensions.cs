using Budgetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Helpers
{
    public static class DispayExtensions
    {
        public static string GetDisplayAmountText(this decimal value)
        {
            return value.ToString("C");
        }

        public static string GetDisplayDate(this DateTime value)
        {
            return string.Format(value.ToString(), "d/m/yy");
        }

        public static string GetDisplayDate(this DateTime? value)
        {
            if (value.HasValue)
            {
                return value.Value.GetDisplayDate();
            }
            else
            {
                return "";
            }
        }

        public static string ToMinuteString(this int i)
        {
            if (i < 10)
                return "0" + i.ToString();
            else return i.ToString();
        }

    }

}
