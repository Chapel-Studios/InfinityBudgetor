using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Constants
{
    class AccountTypesConstants
    {
        public static readonly DisplaySet<AccountType> BankAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Bank Account",
            TypeName = "BankAccount"
        };

        public static readonly DisplaySet<AccountType> IncomeSource = new DisplaySet<AccountType>()
        {
            DisplayText = "Income Source",
            TypeName = "IncomeSource"
        };

        public static readonly DisplaySet<AccountType> BillAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Bill Account",
            TypeName = "BillAccount"
        };

        public static readonly DisplaySet<AccountType> DebtAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Debt Account",
            TypeName = "DebtAccount"
        };
        public const string DefaultIncomeSourceAccountName = "Don't track";
        public const string SystemAccountName = "System Account";

        public enum AccountType
        {

        }
    }
}
