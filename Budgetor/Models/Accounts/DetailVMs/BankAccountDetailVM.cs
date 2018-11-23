using Budgetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class BankAccountDetailVM : AccountDetailVM
    {
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public int? InitialDepositId { get; set; }

        public BankAccountDetailVM()
        {
            base.AccountType = Constants.Accounts.AccountType.BankAccount;
        }

    }
}
