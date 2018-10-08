using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Repo.Models
{
    public class IncomeSourcesListView
    {
        [Key]
        public int IncomeSourceId { get; set; }

        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public DateTime? DateTime_NextTransaction { get; set; }

        public decimal ExpectedAmount { get; set; }

        [StringLength(50)]
        public string PayCycle { get; set; }

    }
}
