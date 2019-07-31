using Budgetor.Models.Transactions;
using Budgetor.Repo;
using Budgetor.Repo.Models;

namespace Budgetor.Overminds
{
    public class OverMindBase
    {
        #region Properties

        protected readonly Repository Repo;

        private AccountsOM _AccountsOM;
        protected AccountsOM AccountsOM
        {
            get
            {
                if (_AccountsOM == null)
                {
                    _AccountsOM = new AccountsOM(Repo);
                }
                return _AccountsOM;
            }
        }

        private TransactionsOM _TransactionsOM;
        protected TransactionsOM TransactionsOM
        {
            get
            {
                if (_TransactionsOM == null)
                {
                    _TransactionsOM = new TransactionsOM(Repo);
                }
                return _TransactionsOM;
            }
        }

        #endregion Properties

        #region Constructors

        public OverMindBase(Repository repo, AccountsOM accountsOM = null, TransactionsOM transactionsOM = null)
        {
            _TransactionsOM = transactionsOM;
            _AccountsOM = accountsOM;
            Repo = repo;
        }

        #endregion Constructors

        #region Mapping

        protected TransactionDetailBase MapRepoTransactionToTransactionBase(Transaction repoTransaction)
        {
            return new TransactionDetailBase(repoTransaction, AccountsOM);
        }

        #endregion Mapping

    }
}
