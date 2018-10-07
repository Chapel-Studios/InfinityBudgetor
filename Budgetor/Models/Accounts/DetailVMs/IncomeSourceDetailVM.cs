using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Accounts
{
    class IncomeSourceDetailVM : AccountDetailVM//, IAccountDetail
    {
        public decimal ExpectedAmount { get; set; }

        public decimal TotalFromSource { get; set; }

        public int DefaultToAccount { get; set; }

        public Scheduling.Schedule Schedule { get; set; }

        public AccountDetailVM RootAccount => MapToAccountDetailVM();

        private AccountDetailVM MapToAccountDetailVM()
        {
            return new AccountDetailVM();
        }
    }
}
