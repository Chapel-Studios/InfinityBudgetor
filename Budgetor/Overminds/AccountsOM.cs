using Budgetor.Models;
using Budgetor.Models.Accounts;
using Budgetor.Repo;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgetor.Overminds
{
    public class AccountsOM
    {
        private Repository _repo;
        private Repository repo
        {
            get
            {
                if (_repo == null)
                {
                    _repo = new Repository();
                }
                return _repo;
            }
        }


        public BankAccountDetailVM GetBankAccountsById(int BankAccountId)
        {
            throw new NotImplementedException();
        }

        #region AccountLists_Tab Calls

        public List<BankAccountListVM> GetBankAccountsList()
        {
            return repo.GetBankAccountsListViews().Select(x => new BankAccountListVM()
            {
                AccountName = x.AccountName,
                Balance = x.Balance,
                DateTime_Created = x.DateTime_Created,
                DateTime_Deactivated = x.DateTime_Deactivated,
                IsActiveCashAccount = x.IsActiveCashAccount,
                IsDefault = x.IsDefault,
                LocalId = x.LocalId
            }).ToList();

        }

        public List<IncomeSourceListVM> GetIncomeSourcesList()
        {
            return repo.GetIncomeSourceListViews().Select(x => new IncomeSourceListVM()
            {
                AccountName = x.AccountName,
                DateTime_Created = x.DateTime_Created,
                DateTime_Deactivated = x.DateTime_Deactivated,
                LocalId = x.LocalId,
                DateTime_NextTransaction = x.DateTime_NextTransaction,
                ExpectedAmount = x.ExpectedAmount,
                PayCycle = x.PayCycle
            }).ToList();

        }

        internal AccountDetailVM SaveAccount(AccountDetailVM account)
        {
            var result = repo.SaveAccount(new Account() {
                AccountName = account.AccountName,
                LocalId = account.LocalId,
                Notes = account.Notes
            });
            return new AccountDetailVM()
            {
                AccountName = result.AccountName,
                Notes = result.Notes,
                LocalId = result.LocalId,
                DateTime_Created = result.DateTime_Created,
                DateTime_Deactivated = result.DateTime_Deactivated
            };
        }

        internal IncomeSourceDetailVM SaveAccount(IncomeSourceDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            var result = repo.SaveAccount(new IncomeSource() {
                Account = baseAccount.LocalId,
                DefaultToAccount = account.DefaultToAccount,
                ExpectedAmount = account.ExpectedAmount,
                Schedule = account.Schedule?.LocalId
            });
            Schedule accountSchedule = result.Schedule.HasValue ? repo.GetSchedule(result.Schedule.Value) : null;

            return new IncomeSourceDetailVM()
            {
                AccountName = baseAccount.AccountName,
                Notes = baseAccount.Notes,
                LocalId = baseAccount.LocalId,
                DateTime_Created = baseAccount.DateTime_Created,
                DateTime_Deactivated = baseAccount.DateTime_Deactivated,
                DefaultToAccount = result.DefaultToAccount,
                ExpectedAmount = result.ExpectedAmount,
                Schedule = new Models.Scheduling.Schedule() {
                    LocalId = accountSchedule.LocalId,
                    Frequency = Constants.Frequency.GetDisplayByTypeName(accountSchedule.Frequency).Enum,
                    DateTime_Created = accountSchedule.DateTime_Created,
                    DateTime_Deactivated = accountSchedule.DateTime_Deactivated,
                    Occurrence_Final = accountSchedule.Occurrence_Final,
                    Occurrence_First = accountSchedule.Occurrence_First,
                    Occurrence_LastConfirmed = accountSchedule.Occurrence_LastConfirmed,
                    Occurrence_LastPlanned = accountSchedule.Occurrence_LastPlanned
                },
                TotalFromSource = result.TotalFromSource,
            };
        }

        #endregion AccountLists Tab Calls
    }
}
