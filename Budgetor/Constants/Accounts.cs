using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Constants
{
    public class Accounts
    {
        public const string DefaultIncomeSourceAccountName = "Don't track";
        public const string DefaultCashAccountName = "Cash";
        public const string SystemAccountName = "System Account";


        public static readonly DisplaySet<AccountType> BankAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Bank Account",
            TypeName = "BankAccount",
            Enum = AccountType.BankAccount,
            HelpText = "These accounts are used for storing money and can be either a checking or savings account or even your personal pigggy bank or matress money."
        };

        public static readonly DisplaySet<AccountType> IncomeSource = new DisplaySet<AccountType>()
        {
            DisplayText = "Income Source",
            TypeName = "IncomeSource",
            Enum = AccountType.IncomeSource,
            HelpText = "Income sources are used for reoccurring revenue like wages from jobs or settlement payments."
        };

        public static readonly DisplaySet<AccountType> BillAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Bill Account",
            TypeName = "BillAccount",
            Enum = AccountType.BillAccount,
            HelpText = "Bill accounts will track regularly occuring bills from utilities to things like yearly subscription fees."
        };

        public static readonly DisplaySet<AccountType> DebtAccount = new DisplaySet<AccountType>()
        {
            DisplayText = "Debt Account",
            TypeName = "DebtAccount",
            Enum = AccountType.DebtAccount,
            HelpText = "Debt account help you catch up on debts with payment plans that help minimize costly fees."
        };

        private static List<DisplaySet<AccountType>> AccountTypes = new List<DisplaySet<AccountType>>()
        {
            BankAccount, IncomeSource, BillAccount, DebtAccount
        };

        public static DisplaySet<AccountType> GetDisplay(AccountType type)
        {
            return AccountTypes.Find(x => x.Enum == type);
        }

        public static DisplaySet<AccountType> GetDisplayByTypeName(string accountType)
        {
            return AccountTypes.Find(x => x.TypeName == accountType);
        }

        public enum AccountType
        {
            //MasterAccount = 0
            BankAccount = 1,
            IncomeSource = 2,
            BillAccount = 3,
            DebtAccount = 4
        }
    }
}
