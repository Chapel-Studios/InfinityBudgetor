namespace Budgetor.Repo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ScheduleFrequencyType
    {
        [Key]
        [StringLength(10)]
        public string FrequencyType { get; set; }

        [Required]
        [StringLength(50)]
        public string FrequencyName { get; set; }
    }
}
