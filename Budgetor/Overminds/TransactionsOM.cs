using Budgetor.Repo.Models;
using Budgetor.Models;
using Budgetor.Constants;

namespace Budgetor.Overminds
{
    class TransactionsOM : OverMindBase
    {
        #region Mapping

        private TransactionBase FromRepoToTransactionBase(Transaction repoTransaction)
        {
            return new TransactionBase()
            {
                Title = repoTransaction.Title,
                Amount = repoTransaction.Amount,
                DateTime_Created = repoTransaction.DateTime_Created,
                DateTime_Occurred = repoTransaction.DateTime_Occurred,
                FromAccount = AccountsOM.GetGenericAccountDetails(repoTransaction.FromAccount),
                ToAccount = AccountsOM.GetGenericAccountDetails(repoTransaction.ToAccount),
                TransactionType = Transactions.GetDisplayByTypeName(repoTransaction.TransactionType).Enum,
                IsConfirmed = repoTransaction.IsConfirmed,
                IsOccurrence = repoTransaction.IsOccurrence,
                IsUserCreated = repoTransaction.IsUserCreated,
                OccerrenceAccount = repoTransaction.OccerrenceAccount.HasValue ? AccountsOM.GetGenericAccountDetails(repoTransaction.OccerrenceAccount.Value) : null,
            };
        }

        #endregion Mapping
    }
}
