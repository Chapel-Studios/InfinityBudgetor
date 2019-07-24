using Budgetor.Models;
using System.Collections.Generic;
using System.Linq;

namespace Budgetor.Constants
{
    public static class Frequency
    {
        private const string _Infrequent = "Infrequent";
        private const string _Weekly = "Weekly";
        private const string _BiWeekly = "BiWeekly";
        private const string _Monthly = "Monthly";
        private const string _Quarterly = "Quarterly";
        private const string _Yearly = "Yearly";
        //public const string Custom = "Custom";

        public static DisplaySet<FrequencyType> Infrequent = new DisplaySet<FrequencyType>()
        {
            DisplayText = "InFrequent",
            HelpText = "These only occur when you create them.",
            TypeName = _Infrequent,
            EnumOption = FrequencyType.Infrequent
        };

        public static DisplaySet<FrequencyType> Weekly = new DisplaySet<FrequencyType>()
        {
            DisplayText = "Weekly",
            HelpText = "These occur once a week based on your starting date.",
            TypeName = _Weekly,
            EnumOption = FrequencyType.Weekly
        };

        public static DisplaySet<FrequencyType> BiWeekly = new DisplaySet<FrequencyType>()
        {
            DisplayText = "BiWeekly",
            HelpText = "These only occur once every other week based on your starting date.",
            TypeName = _BiWeekly,
            EnumOption = FrequencyType.BiWeekly
        };

        public static DisplaySet<FrequencyType> Monthly = new DisplaySet<FrequencyType>()
        {
            DisplayText = "Monthly",
            HelpText = "These occur once a month on the same day of the month as the first transaction. If the month doesn't have enough days, it will take place on the last day of the month.",
            TypeName = _Monthly,
            EnumOption = FrequencyType.Monthly
        };

        public static DisplaySet<FrequencyType> Quarterly = new DisplaySet<FrequencyType>()
        {
            DisplayText = "Quarterly",
            HelpText = "These only occur once every 3 months on the same day of the month as the first transaction. If the month doesn't have enough days, it will take place on the last day of the month.",
            TypeName = _Quarterly,
            EnumOption = FrequencyType.Quarterly
        };

        public static DisplaySet<FrequencyType> Yearly = new DisplaySet<FrequencyType>()
        {
            DisplayText = "Yearly",
            HelpText = "These only occur once every year on it's anniversary.",
            TypeName = _Yearly,
            EnumOption = FrequencyType.Yearly
        };

        private static List<DisplaySet<FrequencyType>> Frequencies = new List<DisplaySet<FrequencyType>>()
        {
            Infrequent, Weekly, BiWeekly, Monthly, Quarterly, Yearly
        };

        public static DisplaySet<FrequencyType> GetDisplay(FrequencyType frequency)
        {
            return Frequencies.Find(x => x.EnumOption == frequency);
        }

        public static DisplaySet<FrequencyType> GetDisplayByTypeName(string frequency)
        {
            return Frequencies.Find(x => x.TypeName == frequency);
        }

        public static List<FrequencyComboBoxItem> GetFrequencyComboBoxItems()
        {
            return Frequencies.Select(x => new FrequencyComboBoxItem(x)).ToList();
        }

    }

    public enum FrequencyType
    {
        Infrequent = 0,
        Weekly = 7,
        BiWeekly = 14,
        Monthly = 30,
        Quarterly = 90,
        Yearly = 365
    }
}
