namespace Budgetor.Repo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Budgetor.Models.Scheduling;

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

        public bool? UsesLastDayOfTheMonth { get; set; }

        public string IgnoreWeekendsOption { get; set; }

        public string DateString { get; set; }

        public Schedule()
        {

        }

        public Schedule(Schedule_Base sched)
        {
            this.LocalId = sched.ScheduleId;
            this.Frequency = sched.Frequency.ToString();
            this.Occurrence_First = sched.Occurrence_First;
            this.Occurrence_LastConfirmed = sched.Occurrence_LastConfirmed;
            this.Occurrence_LastPlanned = sched.Occurrence_LastPlanned;
            this.Occurrence_Final = sched.Occurrence_Final;
            this.IsAutoConfirm = sched.IsAutoConfirm;
            this.DateTime_Created = sched.DateTime_Created;
            this.DateTime_Deactivated = sched.DateTime_Deactivated;
            this.HasCustomTransactionTime = sched.HasCustomTransactionTime;
            this.UsesLastDayOfTheMonth = sched.UsesLastDayOfTheMonth;
            this.IgnoreWeekendsOption = sched.IgnoreWeekendsOption ?? string.Empty;
            this.DateString = string.Join(",", sched.SelectedDates);
        }
    }
}
