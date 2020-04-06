
using Budgetor.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class FrequencyComboBoxItem : GenericComboBoxItem
    {
        #region Properties

        public string Frequency_Display
        {
            get
            {
                return Display;
            }
            set
            {
                Display = value;
            }
        }

        public int FrequencyId
        {
            get
            {
                return IntValue.HasValue ? IntValue.Value : -1;
            }
        }

        private FrequencyType _SetFrequency;
        public FrequencyType SetFrequency
        {
            get => _SetFrequency;
            set
            {
                _StringValue = Frequency.GetDisplay(value).TypeName;
                IntValue = (int)value;
                _SetFrequency = value;
            }
        }

        private string _StringValue;

        public override string StringValue
        {
            get => _StringValue;
            set
            {
                _SetFrequency = Frequency.GetDisplayByTypeName(value).EnumOption;
                _StringValue = value;
            }
        }

        public bool IsDefault { get; set; }

        #endregion Properties

        public FrequencyComboBoxItem(string stringValue) : base(stringValue)
        {
        }

        public FrequencyComboBoxItem(DisplaySet<FrequencyType> frequencySet)
        {
            _SetFrequency = frequencySet.EnumOption;
            IntValue = frequencySet.IntValue;
            _StringValue = frequencySet.Display;
            Display = _StringValue;
        }
    }
}
