using Budgetor.Models;
using Budgetor.Models.Accounts;
using Budgetor.Models.Scheduling;
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


        public BankAccountDetailVM GetBankAccountById(int BankAccountId)
        {
            throw new NotImplementedException();
        }

        #region AccountLists_Tab Calls

        public List<BankAccountListVM> GetBankAccountsList()
        {
            return repo.GetBankAccountsListViews().Select(x => new BankAccountListVM()
            {
                AccountName = x.AccountName,
                //todo: need to know how to get this
                //Balance = x.Balance,
                DateTime_Created = x.DateTime_Created,
                DateTime_Deactivated = x.DateTime_Deactivated,
                IsActiveCashAccount = x.IsActiveCashAccount,
                IsDefault = x.IsDefault,
                AccountId = x.AccountId,
                DepositAccountId = x.DepositAccountId
            }).ToList();

        }

        public List<IncomeSourceListVM> GetIncomeSourcesList()
        {
            return repo.GetIncomeSourceListViews().Select(x => new IncomeSourceListVM()
            {
                AccountName = x.AccountName,
                DateTime_Created = x.DateTime_Created,
                DateTime_Deactivated = x.DateTime_Deactivated,
                AccountId = x.AccountId,
                IncomeSourceId = x.IncomeSourceId,
                DateTime_NextTransaction = x.DateTime_NextTransaction,
                ExpectedAmount = x.ExpectedAmount,
                PayCycle = x.PayCycle
            }).ToList();

        }

        #endregion AccountLists Tab Calls

        #region Saves

        internal AccountDetailVM SaveAccount(AccountDetailVM account)
        {
            var result = repo.SaveAccount(AccountFromDetailVM(account));
            return AccountToDetailVM(result);
        }

        internal IncomeSourceDetailVM SaveAccount(IncomeSourceDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            account.AccountId = baseAccount.AccountId;
            IncomeSource result = repo.SaveAccount(IncomeSourceFromDetailVM(account));

            return IncomeSourceToDetailVM(result, baseAccount);
        }

        internal BankAccountDetailVM SaveAccount(BankAccountDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            var result = repo.SaveAccount(new DepositAccount()
            {
                LocalId = account.DepositAccountId,
                AccountId = baseAccount.AccountId,
                InitialDepositId = account.InitialDepositId,
                IsActiveCashAccount = account.IsActiveCashAccount,
                IsDefault = account.IsDefault
            });

            return new BankAccountDetailVM()
            {
                AccountName = baseAccount.AccountName,
                Notes = baseAccount.Notes,
                DepositAccountId = result.LocalId,
                AccountId = baseAccount.AccountId,
                DateTime_Created = baseAccount.DateTime_Created,
                DateTime_Deactivated = baseAccount.DateTime_Deactivated,
                IsDefault = account.IsDefault,
                IsActiveCashAccount = account.IsActiveCashAccount,
                InitialDepositId = account.InitialDepositId,
            };
        }

        #endregion Saves

        #region FYE Calls

        internal bool SetAccountToSystem(int account)
        {
            var result = false;

            try
            {
                repo.SetAccountToSystem(account);
                result = true;
            }
            catch
            {
                //todo: need logging!!!!!!
            }

            return result;
        }

        #endregion FYE Calls

        #region mapping

        private AccountDetailVM AccountToDetailVM(Account account)
        {
            return new AccountDetailVM()
            {
                AccountName = account.AccountName,
                Notes = account.Notes,
                AccountId = account.LocalId,
                DateTime_Created = account.DateTime_Created,
                DateTime_Deactivated = account.DateTime_Deactivated
            };

        }

        private Account AccountFromDetailVM(AccountDetailVM account)
        {
            return new Account()
            {
                AccountName = account.AccountName,
                LocalId = account.AccountId,
                Notes = account.Notes
            };
        }

        private IncomeSource IncomeSourceFromDetailVM(IncomeSourceDetailVM source)
        {
            return new IncomeSource()
            {
                LocalId = source.IncomeSourceId,
                AccountId = source.AccountId,
                DefaultToAccountId = source.DefaultToAccountId,
                ExpectedAmount = source.ExpectedAmount,
                ScheduleId = source.Schedule?.LocalId
            };
        }

        private IncomeSourceDetailVM IncomeSourceToDetailVM(IncomeSource source, AccountDetailVM baseAccount)
        {

            ScheduleVM accountSchedule = null;
            if (source.ScheduleId.HasValue)
            {
                var repoSched = repo.GetSchedule(source.ScheduleId.Value);
                accountSchedule = ScheduleToVM(repoSched);
            }

            return new IncomeSourceDetailVM()
            {
                AccountName = baseAccount.AccountName,
                Notes = baseAccount.Notes,
                IncomeSourceId = source.LocalId,
                AccountId = baseAccount.AccountId,
                DateTime_Created = baseAccount.DateTime_Created,
                DateTime_Deactivated = baseAccount.DateTime_Deactivated,
                DefaultToAccountId = source.DefaultToAccountId,
                ExpectedAmount = source.ExpectedAmount,
                Schedule = accountSchedule,
                TotalFromSource = source.TotalFromSource,
            };
        }

        private ScheduleVM ScheduleToVM(Schedule schedule)
        {
            return new ScheduleVM()
            {
                LocalId = schedule.LocalId,
                Frequency = Constants.Frequency.GetDisplayByTypeName(schedule.Frequency).Enum,
                DateTime_Created = schedule.DateTime_Created,
                DateTime_Deactivated = schedule.DateTime_Deactivated,
                Occurrence_Final = schedule.Occurrence_Final,
                Occurrence_First = schedule.Occurrence_First,
                Occurrence_LastConfirmed = schedule.Occurrence_LastConfirmed,
                Occurrence_LastPlanned = schedule.Occurrence_LastPlanned
            };
        }

        #endregion mapping


    }
}
