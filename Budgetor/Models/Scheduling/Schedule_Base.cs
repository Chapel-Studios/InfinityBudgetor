using Budgetor.Repo.Models;
using System;
using Budgetor.Constants;

namespace Budgetor.Models
{
    public class Schedule_Base
    {
        public int LocalId { get; set; }

        public FrequencyType Frequency { get; set; }

        public DateTime Occurrence_First { get; set; }

        public DateTime? Occurrence_LastConfirmed { get; set; }

        public DateTime? Occurrence_LastPlanned { get; set; }

        public DateTime? Occurrence_Final { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public Schedule_Base()
        {

        }

        public Schedule_Base(Schedule schedule)
        {
            if (schedule != null)
            {
                LocalId = schedule.LocalId;
                Frequency = Constants.Frequency.GetDisplayByTypeName(schedule.Frequency).EnumOption;
                DateTime_Created = schedule.DateTime_Created;
                DateTime_Deactivated = schedule.DateTime_Deactivated;
                Occurrence_Final = schedule.Occurrence_Final;
                Occurrence_First = schedule.Occurrence_First;
                Occurrence_LastConfirmed = schedule.Occurrence_LastConfirmed;
                Occurrence_LastPlanned = schedule.Occurrence_LastPlanned;
            }
        }

    }
}
