using Budgetor.Repo.Models;
using Budgetor.Models;
using Budgetor.Constants;
using System;

namespace Budgetor.Overminds
{
    public class TransactionsOM : OverMindBase
    {
        #region Mapping

        private TransactionDetailBase FromRepoToTransactionBase(Transaction repoTransaction)
        {
            return new TransactionDetailBase(Transactions.GetDisplayByTypeName(repoTransaction.TransactionType).Enum)
            {
                Title = repoTransaction.Title,
                Amount = repoTransaction.Amount,
                DateTime_Created = repoTransaction.DateTime_Created,
                DateTime_Occurred = repoTransaction.DateTime_Occurred,
                FromAccount = AccountsOM.GetGenericAccountDetails(repoTransaction.FromAccount),
                ToAccount = AccountsOM.GetGenericAccountDetails(repoTransaction.ToAccount),
                IsConfirmed = repoTransaction.IsConfirmed,
                IsUserCreated = repoTransaction.IsUserCreated,
                OccerrenceAccount = repoTransaction.OccerrenceAccount.HasValue ? AccountsOM.GetGenericAccountDetails(repoTransaction.OccerrenceAccount.Value) : null,
            };
        }

        internal TransactionModalVM GetTransactionModalVM(int? id)
        {
            return new TransactionModalVM();
        }

        #endregion Mapping
    }
}
