using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budgetor.Constants.Accounts;

namespace Budgetor.Models
{
    public class AccountBasicInfo
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public AccountType AccountType { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }
    }
}
