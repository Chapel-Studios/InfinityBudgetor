using Budgetor.Models.Contracts;
using Budgetor.Models.Shared;
using System.Collections.Generic;

namespace Budgetor.Helpers.Utilities
{
    public static class ScheduleHelper
    {
        public static List<IComboBoxItem> GetIgnoreWeekendOptions()
        {
            return new List<IComboBoxItem>()
            {
                new GenericComboBoxItem("Monday"),
                new GenericComboBoxItem("Friday")
            };
        }
    }
}
