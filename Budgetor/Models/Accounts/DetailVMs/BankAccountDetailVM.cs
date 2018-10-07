using Budgetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Accounts
{
    public class BankAccountDetailVM : AccountDetailVM
    {
        public int Account { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public decimal InitialBalance { get; set; }

        public int? InitialDeposit { get; set; }

    }
}
