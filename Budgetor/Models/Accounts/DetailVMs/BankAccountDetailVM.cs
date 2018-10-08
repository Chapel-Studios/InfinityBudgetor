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
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public int? InitialDepositId { get; set; }

    }
}
