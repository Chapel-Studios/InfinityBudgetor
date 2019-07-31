using Budgetor.Repo.Models;
using Budgetor.Models.Transactions;
using Budgetor.Models.Shared;
using Budgetor.Constants;
using System;
using System.Collections.Generic;
using Budgetor.Models.Contracts;
using System.Linq;
using System.Collections.ObjectModel;
using Budgetor.Helpers.Extensions;
using Budgetor.Repo;
using Budgetor.Models.Scheduling;
using Budgetor.Helpers.Utilities;

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

        #region Transactions

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

        internal TransactionDetailBase SaveTransaction(TransactionSaveInfo transaction)
        {
            var result = Repo.SaveTransaction(new Transaction(transaction));
            return new TransactionDetailBase(result, AccountsOM);
        }

        private ObservableCollection<TransactionsListItemVM> GetTransactionsListItems(DateTime beginning, DateTime cutOff)
        {
            return new ObservableCollection<TransactionsListItemVM>(Repo.GetTransactions(beginning, cutOff).Select(x => new TransactionsListItemVM(MapRepoTransactionToTransactionBase(x)))
                    .ToList());
        }

        #endregion Transactions

        #region Schedules

        internal ManageScheduleVM GetManageScheduleVM(int? id)
        {
            Schedule_Base schedule = null;
            if (id.HasValue && id.Value != 0)
                schedule = new Schedule_Base(Repo.GetSchedule(id.Value));
            return new ManageScheduleVM(schedule, 
                                        Frequency.GetFrequencyComboBoxItems(),
                                        Time.GetHoursComboBoxItems(),
                                        Time.GetMeridianComboBoxItems(),
                                        Time.GetTimeZonesComboBoxItems(),
                                        ScheduleHelper.GetIgnoreWeekendOptions()
            );
        }

        internal Schedule_Base SaveSchedule(Schedule_Base sched)
        {
            return new Schedule_Base(Repo.SaveSchedule(new Schedule(sched)));
        }

        #endregion Schedules

    }
}
