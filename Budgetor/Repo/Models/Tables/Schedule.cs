namespace Budgetor.Repo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Budgetor.Models;

    public partial class Schedule
    {
        [Key]
        public int LocalId { get; set; }

        [Required]
        [StringLength(10)]
        public string Frequency { get; set; }

        public DateTime Occurrence_First { get; set; }

        public DateTime? Occurrence_LastConfirmed { get; set; }

        public DateTime? Occurrence_LastPlanned { get; set; }

        public DateTime? Occurrence_Final { get; set; }

        public bool IsAutoConfirm { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool HasCustomTransactionTime { get; set; }



        public Schedule()
        {

        }

        public Schedule(Schedule_Base sched)
        {
            this.LocalId = sched.ScheduleId;
            this.DateTime_Created = sched.DateTime_Created;
            this.DateTime_Deactivated = sched.DateTime_Deactivated;
            this.Frequency = sched.Frequency.ToString();
            this.Occurrence_LastPlanned = sched.Occurrence_LastPlanned;
            this.Occurrence_LastConfirmed = sched.Occurrence_LastConfirmed;
            this.Occurrence_First = sched.Occurrence_First;
            this.Occurrence_Final = sched.Occurrence_Final;
            this.HasCustomTransactionTime = sched.HasCustomTransactionTime;
            this.IsAutoConfirm = sched.IsAutoConfirm;
        }
    }
}
