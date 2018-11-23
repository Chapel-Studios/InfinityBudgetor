using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class IncomeSourceDetailVM : AccountDetailVM//, IAccountDetail
    {
        public int IncomeSourceId { get; set; }

        public decimal ExpectedAmount { get; set; }

        public decimal TotalFromSource { get; set; }
        
        public int? DefaultToAccountId { get; set; }

        public Scheduling.ScheduleVM Schedule { get; set; }

        public IncomeSourceDetailVM()
        {
            base.AccountType = Constants.Accounts.AccountType.IncomeSource;
        }
    }
}
