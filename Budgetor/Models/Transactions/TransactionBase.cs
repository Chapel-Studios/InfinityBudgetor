using Budgetor.Models.Contracts;
using System;
using Budgetor.Constants;
using Budgetor.Repo.Models;
using Budgetor.Overminds;
using Budgetor.Models.Accounts;

namespace Budgetor.Models.Transactions
{
    public class TransactionDetailBase : ITransactionDetail
    {
        public string Title { get; set; }

        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public AccountBasicInfo ToAccount { get; set; }

        public AccountBasicInfo FromAccount { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public AccountBasicInfo OccerrenceAccount { get; set; }

        public string Notes { get; set; }

        public TransactionDetailBase()
        {
            DateTime_Occurred = DateTime.Now;
        }

        public TransactionDetailBase(TransactionType transactionType)
        {
            TransactionType = transactionType;
            DateTime_Occurred = DateTime.Now;
        }

        public TransactionDetailBase(Transaction repoTransaction, AccountsOM om)
        {
            AccountBasicInfo toAccount = repoTransaction.ToAccount.HasValue
                                ? om.GetGenericAccountDetails(repoTransaction.ToAccount.Value)
                                : null;
            AccountBasicInfo fromAccount = om.GetGenericAccountDetails(repoTransaction.FromAccount);
            AccountBasicInfo occerrenceAccount = repoTransaction.OccerrenceAccount.HasValue
                                ? om.GetGenericAccountDetails(repoTransaction.OccerrenceAccount.Value)
                                : null;
            
            Title = repoTransaction.Title;
            TransactionType = Constants.Transactions.GetDisplayByTypeName(repoTransaction.TransactionType).EnumOption;
            Amount = repoTransaction.Amount;
            ToAccount = toAccount;
            FromAccount = fromAccount;
            DateTime_Created = repoTransaction.DateTime_Created;
            DateTime_Occurred = repoTransaction.DateTime_Occurred;
            IsUserCreated = repoTransaction.IsUserCreated;
            IsConfirmed = repoTransaction.IsConfirmed;
            OccerrenceAccount = occerrenceAccount;
            Notes = repoTransaction.Notes;
            TransactionId = repoTransaction.LocalId;
        }
    }
}
