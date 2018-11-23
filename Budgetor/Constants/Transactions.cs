using System.Collections.Generic;

namespace Budgetor.Constants
{
    public class Transactions
    {
        public static readonly DisplaySet<TransactionType> Purchase = new DisplaySet<TransactionType>()
        {
            DisplayText = "Purchase",
            TypeName = "Purchase",
            Enum = TransactionType.Purchase,
            HelpText = ""
        };

        public static readonly DisplaySet<TransactionType> Deposit = new DisplaySet<TransactionType>()
        {
            DisplayText = "Deposit",
            TypeName = "Deposit",
            Enum = TransactionType.Deposit,
            HelpText = ""
        };

        private static List<DisplaySet<TransactionType>> TransactionTypes = new List<DisplaySet<TransactionType>>()
        {
            Purchase, Deposit
        };

        public static DisplaySet<TransactionType> GetDisplay(TransactionType transactionType)
        {
            return TransactionTypes.Find(x => x.Enum == transactionType);
        }

        public static DisplaySet<TransactionType> GetDisplayByTypeName(string transactionType)
        {
            return TransactionTypes.Find(x => x.TypeName == transactionType);
        }

        public enum TransactionType
        {
            Purchase = 0,
            Deposit = 1
        }


    }
}
