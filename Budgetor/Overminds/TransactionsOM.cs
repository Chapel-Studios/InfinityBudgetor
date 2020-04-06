using Budgetor.Repo.Models;
using Budgetor.Models;
using Budgetor.Constants;
using System;
using System.Collections.Generic;
using Budgetor.Models.Contracts;
using System.Linq;
using System.Collections.ObjectModel;
using Budgetor.Helpers;
using Budgetor.Repo;

namespace Budgetor.Overminds
{
    public class TransactionsOM : OverMindBase
    {
        #region Properties


        #endregion Properties

        #region Constructor

        public TransactionsOM(Repository repo) : base(repo)
        {

        }

        #endregion Constructor


        internal TransactionModalVM GetTransactionModalVM(int? transactionId)
        {
            TransactionDetailBase transaction;
            if (transactionId.HasValue)
            {
                transaction = MapRepoTransactionToTransactionBase(Repo.GetTransactionById(transactionId.Value));
            }
            else
            {
                transaction = new TransactionDetailBase();
            }

            return new TransactionModalVM(transaction,
                                          Transactions.TransactionTypesDropDown,
                                          Time.GetHoursComboBoxItems(),
                                          Time.GetMeridianComboBoxItems(),
                                          Time.GetTimeZonesComboBoxItems(),
                                          new List<IComboBoxItem>() { new GenericComboBoxItem("Not yet Implemented") },
                                          Repo.GetAccountComboBoxInfo(new List<string>(Enum.GetNames(typeof(AccountType)))).ConvertRepoToView(),
                                          transactionId.HasValue
            );
        }

        internal TransactionsTabVM GetTransactionsTabVM()
        {
            // get all transactions for 3 months in either direction to pre populate calendar
            
            return new TransactionsTabVM(GetTransactionsListItems(DateTime.Now.AddMonths(-3), DateTime.Now.AddMonths(3)));
        }

        internal void DeleteTransaction(int transactionId)
        {
            Repo.DeleteTransaction(transactionId);
        }

        internal void SaveTransaction(TransactionSaveInfo initialDeposit)
        {
            Repo.SaveTransaction(new Transaction(initialDeposit));
            // TODO: what does this return?
        }

        private ObservableCollection<TransactionsListItemVM> GetTransactionsListItems(DateTime beginning, DateTime cutOff)
        {
            return new ObservableCollection<TransactionsListItemVM>(Repo.GetTransactions(beginning, cutOff).Select(x => new TransactionsListItemVM(MapRepoTransactionToTransactionBase(x)))
                    .ToList());
        }
    }
}
