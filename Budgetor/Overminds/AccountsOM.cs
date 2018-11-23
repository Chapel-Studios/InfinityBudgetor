using Budgetor.Models;
using Budgetor.Models.Contracts;
using Budgetor.Models.Scheduling;
using Budgetor.Repo;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Budgetor.Overminds
{
    public class AccountsOM : OverMindBase
    {
        public AccountDetailVM GetGenericAccountDetails(int accountId)
        {
            return AccountToDetailVM(Repo.GetAccount(accountId));
        }
        public BankAccountDetailVM GetBankAccountById(int BankAccountId)
        {
            throw new NotImplementedException();
        }

        #region AccountLists_Tab Calls

        public List<BankAccountsListItemVM> GetBankAccountsList()
        {
            return Repo.GetBankAccountsListViews().Select(x => new BankAccountsListItemVM()
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
            var result = Repo.GetIncomeSourceListViews().Select(x => new IncomeSourceListVM()
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
            if (result.Count == 0)
            {
                result.Add(new IncomeSourceListVM()
                {
                    AccountId = -1,
                    IncomeSourceId = -1,
                    AccountName = "No you currently do not have any income source accounts."
                });
            }
            return result;
        }

        #endregion AccountLists Tab Calls

        #region Saves

        internal AccountDetailVM SaveAccount(AccountDetailVM account)
        {
            var result = Repo.SaveAccount(AccountFromDetailVM(account));
            return AccountToDetailVM(result);
        }

        internal IncomeSourceDetailVM SaveAccount(IncomeSourceDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            account.AccountId = baseAccount.AccountId;
            IncomeSource result = Repo.SaveAccount(IncomeSourceFromDetailVM(account));

            return IncomeSourceToDetailVM(result, baseAccount);
        }

        internal BankAccountDetailVM SaveAccount(BankAccountDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            var result = Repo.SaveAccount(new DepositAccount()
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
                Repo.SetAccountToSystem(account);
                result = true;
            }
            catch
            {
                //todo: need logging!!!!!!
            }

            return result;
        }

        #endregion FYE Calls

        #region Mapping

        private AccountDetailVM AccountToDetailVM(Account account)
        {
            return new AccountDetailVM()
            {
                AccountName = account.AccountName,
                Notes = account.Notes,
                AccountId = account.LocalId,
                DateTime_Created = account.DateTime_Created,
                DateTime_Deactivated = account.DateTime_Deactivated,
                AccountType = Constants.Accounts.GetDisplayByTypeName(account.AccountType).Enum
            };

        }

        private Account AccountFromDetailVM(AccountDetailVM account)
        {
            return new Account()
            {
                AccountName = account.AccountName,
                LocalId = account.AccountId,
                Notes = account.Notes,
                AccountType = Constants.Accounts.GetDisplay(account.AccountType).TypeName
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
                var repoSched = Repo.GetSchedule(source.ScheduleId.Value);
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

        #endregion Mapping


    }
}
