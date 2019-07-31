using Budgetor.Models;
using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Shared
{
    public class DisplaySet<T> : IComboBoxItem where T : Enum
    {
        public string DisplayText;
        public string HelpText;
        public T EnumOption;
        public string TypeName;

        public string Display
        {
            get { return DisplayText; }
            set { DisplayText = value; }
        }
        public int? IntValue
        {
            get { return (int)(object)EnumOption; }
            set { EnumOption = (T)(object)value; }
        }
        public string StringValue
        {
            get { return TypeName; }
            set { TypeName = value; }
        }
    }
}
