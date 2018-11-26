using Budgetor.Models.Contracts;
using System.Collections.Generic;

namespace Budgetor.Constants
{
    public class Transactions
    {
        public static readonly DisplaySet<TransactionType> Purchase = new DisplaySet<TransactionType>()
        {
            DisplayText = "Purchase",
            TypeName = "Purchase",
            EnumOption = TransactionType.Purchase,
            HelpText = ""
        };

        public static readonly DisplaySet<TransactionType> Deposit = new DisplaySet<TransactionType>()
        {
            DisplayText = "Deposit",
            TypeName = "Deposit",
            EnumOption = TransactionType.Deposit,
            HelpText = ""
        };

        public static List<DisplaySet<TransactionType>> TransactionTypes = new List<DisplaySet<TransactionType>>()
        {
            Purchase, Deposit
        };

        public static List<IComboBoxItem> TransactionTypesDropDown = new List<IComboBoxItem>()
        {
            Purchase, Deposit
        };

        public static DisplaySet<TransactionType> GetDisplay(TransactionType transactionType)
        {
            return TransactionTypes.Find(x => x.EnumOption == transactionType);
        }

        public static DisplaySet<TransactionType> GetDisplayByTypeName(string transactionType)
        {
            return TransactionTypes.Find(x => x.TypeName == transactionType);
        }

    }

    public enum TransactionType
    {
        Purchase = 0,
        Deposit = 1
    }

}
