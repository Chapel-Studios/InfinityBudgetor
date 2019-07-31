using Budgetor.Repo.Models;
using System;
using Budgetor.Constants;
using System.Collections.Generic;
using Budgetor.Helpers.Extensions;

namespace Budgetor.Models.Scheduling
{
    public class Schedule_Base
    {
        public int ScheduleId { get; set; }

        public FrequencyType Frequency { get; set; }

        public DateTime Occurrence_First { get; set; }

        public DateTime? Occurrence_LastConfirmed { get; set; }

        public DateTime? Occurrence_LastPlanned { get; set; }

        public DateTime? Occurrence_Final { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool HasCustomTransactionTime { get; set; }

        public bool IsAutoConfirm { get; set; }

        public bool? UsesLastDayOfTheMonth { get; set; }

        public string IgnoreWeekendsOption { get; set; }

        public List<int> SelectedDates { get; set; }



        public Schedule_Base()
        {
            Occurrence_First = DateTime.UtcNow;
            SelectedDates = new List<int>();
        }

        public Schedule_Base(Schedule schedule)
        {
            if (schedule != null)
            {
                ScheduleId = schedule.LocalId;
                Frequency = Constants.Frequency.GetDisplayByTypeName(schedule.Frequency).EnumOption;
                Occurrence_First = schedule.Occurrence_First;
                Occurrence_LastConfirmed = schedule.Occurrence_LastConfirmed;
                Occurrence_LastPlanned = schedule.Occurrence_LastPlanned;
                Occurrence_Final = schedule.Occurrence_Final;
                DateTime_Created = schedule.DateTime_Created;
                DateTime_Deactivated = schedule.DateTime_Deactivated;
                HasCustomTransactionTime = schedule.HasCustomTransactionTime;
                IsAutoConfirm = schedule.IsAutoConfirm;
                UsesLastDayOfTheMonth = schedule.UsesLastDayOfTheMonth;
                IgnoreWeekendsOption = schedule.IgnoreWeekendsOption;
                SelectedDates = schedule.DateString.DeriveIntList(',');
            }
            else
            {
                Occurrence_First = DateTime.UtcNow;
                SelectedDates = new List<int>();
            }
        }

    }
}
