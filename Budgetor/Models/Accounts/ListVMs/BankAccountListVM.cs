using Budgetor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class BankAccountsListItemVM : AccountListItemBaseVM
    {
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public decimal Balance { get; set; }

        public string DisplayBalance => DispayFormatHelper.GetDisplayAmountText(this.Balance);

    }
}
