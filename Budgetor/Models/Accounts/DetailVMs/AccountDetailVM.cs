using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Accounts
{
    public class AccountDetailVM
    {
        public int LocalId { get; set; }

        public string AccountName { get; set; }

        public string Notes { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

    }
}
