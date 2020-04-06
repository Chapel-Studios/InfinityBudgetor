using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Repo.Models
{
    public class IncomeSource_DetailView
    {
        public int IncomeSourceId { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string Notes { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool IsSystem { get; set; }

        public int? DefaultToAccountId { get; set; }

        public decimal ExpectedAmount { get; set; }

        public int? ScheduleId { get; set; }

        public decimal TotalFromSource { get; set; }

    }
}
