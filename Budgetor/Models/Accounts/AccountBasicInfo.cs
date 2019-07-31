using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetor.Constants;

namespace Budgetor.Models.Accounts
{
    public class AccountBasicInfo
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public readonly AccountType AccountType;

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public AccountBasicInfo(AccountType accountType)
        {
            AccountType = accountType;
        }
    }
}
