using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class GenericComboBoxItem : IComboBoxItem
    {
        public virtual string Display { get; set; }
        public virtual int? IntValue { get; set; }
        public virtual string StringValue { get; set; }

        public GenericComboBoxItem()
        {

        }

        public GenericComboBoxItem(string stringValue)
        {
            Display = stringValue;
            StringValue = stringValue;
            IntValue = null;
        }

        public GenericComboBoxItem(int? intVal)
        {
            Display = intVal.ToString();
            StringValue = intVal.ToString();
            IntValue = intVal;
        }

        public GenericComboBoxItem(string display, int? intVal)
        {
            Display = display;
            StringValue = display;
            IntValue = intVal;
        }

        public GenericComboBoxItem(string display, int? intVal, string stringVal)
        {
            Display = display;
            StringValue = stringVal;
            IntValue = intVal;
        }
    }
}
