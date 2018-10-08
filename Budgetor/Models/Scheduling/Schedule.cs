using System;
using static Budgetor.Constants.Frequency;

namespace Budgetor.Models.Scheduling
{
    public class ScheduleVM
    {
        public int LocalId { get; set; }

        public FrequencyType Frequency { get; set; }

        public DateTime Occurrence_First { get; set; }

        public DateTime? Occurrence_LastConfirmed { get; set; }

        public DateTime? Occurrence_LastPlanned { get; set; }

        public DateTime? Occurrence_Final { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }
    }
}
