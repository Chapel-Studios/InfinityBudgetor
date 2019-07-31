using Budgetor.Helpers.Extensions;
using Budgetor.Models.Contracts;
using Budgetor.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgetor.Constants
{
    // Useful links:
    // https://www.timetemperature.com/tzus/gmt_united_states.shtml
    // https://www.timeanddate.com/time/zone/usa

    public class Time
    {

        public static List<IComboBoxItem> GetHoursComboBoxItems(bool use24HourClock = false)
        {
            var result = new List<IComboBoxItem>();
            var hours = 12;
            if (use24HourClock)
                hours += 12;
            for (int i = 1; i <= hours; i++)
            {
                result.Add(new GenericComboBoxItem(i));
            }
            return result;
        }

        public static List<IComboBoxItem> GetMinutesComboBoxItems(int increment = 1)
        {
            var result = new List<IComboBoxItem>();
            for (int i = increment; i <= 60; i += increment)
            {
                result.Add(new GenericComboBoxItem(i));
            }
            return result;
        }

        public static List<IComboBoxItem> GetMeridianComboBoxItems(bool getUpperCase = true)
        {
            if (getUpperCase)
            {
                return new List<IComboBoxItem>()
                {
                    new GenericComboBoxItem("AM"),
                    new GenericComboBoxItem("PM")
                };
            }
            else
            {
                return new List<IComboBoxItem>()
                {
                    new GenericComboBoxItem("am"),
                    new GenericComboBoxItem("pm")
                };
            }
        }

        public static List<IComboBoxItem> GetTimeZonesComboBoxItems()
        {
            var values = Enum.GetValues(typeof(TimeZone)).Cast<TimeZone>();
            var result = new List<IComboBoxItem>();
            foreach(var value in values)
            {
                var attr = value.GetAttribute<StandardTimeInfoAttribute>();
                result.Add(new GenericComboBoxItem(attr.DisplayText, (int)value));
            }
            return result;
        }
    }

    public enum TimeZone
    {
        [StandardTimeInfo(-600, "Hawaii", "HST", "Aleutian(Hawaii) Standard Time (UTC -10:00)")]
        Hawaii,

        [StandardTimeInfo(-540, "Alaska", "AKST", "Alaska Standard Time (UTC -9:00)")]
        [DaylightTimeInfo(-480, "Alaska", "AKDT", "Alaska Daylight Time (UTC -8:00)")]
        Alaska,

        [StandardTimeInfo(-480, "Pacific", "PST", "Pacific Standard Time (UTC -8:00)")]
        [DaylightTimeInfo(-420, "Pacific", "PDT", "Pacific Daylight Time (UTC -7:00)")]
        Pacific,

        [StandardTimeInfo(-420, "Mountain(Arizona)", "MST(AZ)", "Mountain(Arizona) Standard Time (UTC -7:00)")]
        Arizona,

        [StandardTimeInfo(-420, "Mountain", "MST", "Mountain Standard Time (UTC -7:00)")]
        [DaylightTimeInfo(-360, "Mountain", "MDT", "Mountain Daylight Time (UTC -6:00)")]
        Mountain,

        [StandardTimeInfo(-360, "Central", "CST", "Central Standard Time (UTC -6:00)")]
        [DaylightTimeInfo(-300, "Central", "CDT", "Central Daylight Time (UTC -5:00)")]
        Central,

        [StandardTimeInfo(-300, "Eastern", "EST", "Eastern Standard Time (UTC -5:00)")]
        [DaylightTimeInfo(-240, "Eastern", "EDT", "Eastern Daylight Time (UTC -4:00)")]
        Eastern,

        [StandardTimeInfo(-480, "Atlantic", "AST", "Atlantic Standard Time (UTC -4:00)")]
        Atlantic
    }

    public interface ITimeZoneInfoAttribute
    {
        int OffsetMinutes { get; }
        string DisplayText { get; }
        string Abbreviation { get; }
        string FullDescription { get; }
    }

    public class TimeZoneInfoAttribute : Attribute, ITimeZoneInfoAttribute
    {
        public int OffsetMinutes { get; private set; }

        public string DisplayText { get; private set; }

        public string Abbreviation { get; private set; }

        public string FullDescription { get; private set; }

        internal TimeZoneInfoAttribute(int offsetMinutes, string displayText, string abbreviation, string fullDescription)
        {
            OffsetMinutes = offsetMinutes;
            DisplayText = displayText;
            Abbreviation = abbreviation;
            FullDescription = fullDescription;
        }
    }

    public class DaylightTimeInfoAttribute : TimeZoneInfoAttribute
    {
        internal DaylightTimeInfoAttribute(int offsetMinutes, string displayText, string abbreviation, string fullDescription) : base(offsetMinutes, displayText, abbreviation, fullDescription)
        {
        }
    }

    public class StandardTimeInfoAttribute : TimeZoneInfoAttribute
    {
        internal StandardTimeInfoAttribute(int offsetMinutes, string displayText, string abbreviation, string fullDescription) : base(offsetMinutes, displayText, abbreviation, fullDescription)
        {
        }
    }
}
