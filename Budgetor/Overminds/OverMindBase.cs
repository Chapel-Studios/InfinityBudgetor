using Budgetor.Models;
using Budgetor.Repo;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Overminds
{
    public class OverMindBase
    {
        private Repository _Repo;
        protected Repository Repo
        {
            get
            {
                if (_Repo == null)
                {
                    _Repo = new Repository();
                }
                return _Repo;
            }
        }

        private AccountsOM _AccountsOM;
        protected AccountsOM AccountsOM
        {
            get
            {
                if (_AccountsOM == null)
                {
                    _AccountsOM = new AccountsOM();
                }
                return _AccountsOM;
            }
        }

        #region Mapping

        protected TransactionDetailBase MapRepoTransactionToTransactionBase(Transaction repoTransaction)
        {
            return new TransactionDetailBase(repoTransaction,
                                            AccountsOM.GetGenericAccountDetails(repoTransaction.ToAccount),
                                            AccountsOM.GetGenericAccountDetails(repoTransaction.FromAccount),
                                            repoTransaction.OccerrenceAccount.HasValue
                                                ? AccountsOM.GetGenericAccountDetails(repoTransaction.OccerrenceAccount.Value)
                                                : null
            );
        }

        #endregion Mapping

    }
}
