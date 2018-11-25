using Budgetor.Models.Contracts;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetor.Constants;

namespace Budgetor.Models
{
    public class TransactionDetailBase : ITransactionDetail
    {
        public string Title { get; set; }

        public Constants.Transactions.TransactionType TransactionType { get; private set; }

        public decimal Amount { get; set; }

        public AccountBasicInfo ToAccount { get; set; }

        public AccountBasicInfo FromAccount { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public AccountBasicInfo OccerrenceAccount { get; set; }

        public TransactionDetailBase()
        {

        }

        public TransactionDetailBase(Constants.Transactions.TransactionType transactionType)
        {
            TransactionType = transactionType;
        }

        public TransactionDetailBase(Transaction repoTransaction, AccountBasicInfo toAccount, AccountBasicInfo fromAccount, AccountBasicInfo occerrenceAccount)
        {
            Title = repoTransaction.Title;
            TransactionType = Constants.Transactions.GetDisplayByTypeName(repoTransaction.TransactionType).Enum;
            Amount = repoTransaction.Amount;
            ToAccount = toAccount;
            FromAccount = fromAccount;
            DateTime_Created = repoTransaction.DateTime_Created;
            DateTime_Occurred = repoTransaction.DateTime_Occurred;
            IsUserCreated = repoTransaction.IsUserCreated;
            IsConfirmed = repoTransaction.IsConfirmed;
            OccerrenceAccount = occerrenceAccount;
        }
    }
}
