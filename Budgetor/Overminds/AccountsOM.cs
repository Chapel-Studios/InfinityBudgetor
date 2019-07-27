using Budgetor.Constants;
using Budgetor.Helpers;
using Budgetor.Models;
using Budgetor.Repo;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Budgetor.Overminds
{
    public class AccountsOM : OverMindBase
    {

        #region Constructor

        public AccountsOM(Repository repo) : base(repo)
        {

        }

        #endregion Constructor

        #region Get Accounts

        public AccountDetailVM GetGenericAccountDetails(int accountId)
        {
            return new AccountDetailVM(Repo.GetAccount(accountId));
        }

        public BankAccountDetailVM GetBankAccountById(int bankAccountId)
        {
            return new BankAccountDetailVM(Repo.GetDepositAccountDetail(bankAccountId));
        }

        public IncomeSourceDetailVM GetIncomeSourceById(int incSourceId)
        {
            IncomeSource_DetailView result = Repo.GetIncomeSourceDetail(incSourceId);
            Schedule sched = result.ScheduleId.HasValue ? Repo.GetSchedule(result.ScheduleId.Value) : null;
            return new IncomeSourceDetailVM(result, sched);
        }

        #endregion Get Accounts

        #region AccountLists_Tab Calls

        public AccountsTabVM GetAcountsTabVM()
        {
            return new AccountsTabVM()
            {
                BankAccounts = new ObservableCollection<BankAccountsListItemVM>(GetBankAccountsList()),
                IncomeSources = new ObservableCollection<IncomeSourceListVM>(GetIncomeSourcesList())
            };
        }

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

        #region ManageBankAccount Window Calls

        public ManageBankAccountVM GetEditBankAccountVM(int? id)
        {
            BankAccountDetailVM bankAccount;
            if (id.HasValue)
            {
                bankAccount = GetBankAccountById(id.Value);
            }
            else
            {
                bankAccount = new BankAccountDetailVM();
            }

            return new ManageBankAccountVM()
            {
                Account = bankAccount,
                IsEditMode = id.HasValue,
                FromAccounts = GetDepositFromList()
            };
        }

        internal List<AccountComboBoxItem> GetDepositFromList()
        {
            return GetAccountComboBoxItem(new List<string>()
            {
                Constants.Accounts.BankAccount.TypeName,
                Constants.Accounts.IncomeSource.TypeName
            });
        }

        internal List<AccountComboBoxItem> GetAccountComboBoxItem(List<string> types)
        {
            return Repo.GetAccountComboBoxInfo(types).Select(x => new AccountComboBoxItem(x)).ToList();
        }

        #endregion ManageBankAccount Window Calls

        #region ManageIncSource Window Calls

        public ManageIncSourceVM GetManageIncSourceVM(int? id)
        {
            IncomeSourceDetailVM incSource;
            if (id.HasValue)
            {
                incSource = GetIncomeSourceById(id.Value);
            }
            else
            {
                incSource = new IncomeSourceDetailVM();
            }

            List<AccountComboBoxItem> tos = GetAccountComboBoxItem(new List<string>()
            {
                Constants.Accounts.BankAccount.TypeName
            });

            return new ManageIncSourceVM()
            {
                Account = incSource,
                IsEditMode = id.HasValue,
                ToAccounts = tos,
                Frequencies = Frequency.GetFrequencyComboBoxItems(),
                SelectedFrequency = (int?)incSource.Schedule?.Frequency ?? 0,
                SelectedToAccount = incSource?.DefaultToAccountId
                    ?? (tos.GetDefaultAccount() ?? 0)
            };
        }

        #endregion ManageIncSource Window Calls

        #region Saves

        internal AccountDetailVM SaveAccount(AccountDetailVM account)
        {
            return new AccountDetailVM(Repo.SaveAccount(new Account(account)));
        }

        internal IncomeSourceDetailVM SaveAccount(IncomeSourceDetailVM account)
        {
            AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
            account.AccountId = baseAccount.AccountId;
            IncomeSource result = Repo.SaveAccount(new IncomeSource(account));
            Schedule accountSchedule = null;
            if (result.ScheduleId.HasValue)
            {
                accountSchedule = Repo.GetSchedule(result.ScheduleId.Value);
            }

            return new IncomeSourceDetailVM(result, baseAccount, accountSchedule);
        }

        internal BankAccountDetailVM SaveAccount(BankAccountDetailVM account)
        {
            // Is this a new account?
            if (account.AccountId == 0)
            {
                // Create the base account to get an accountId
                AccountDetailVM baseAccount = SaveAccount((AccountDetailVM)account);
                account.AccountId = baseAccount.AccountId;
            }

            
            var result = Repo.SaveAccount(new DepositAccount(account));
            Transaction transaction = null;
            if (result.InitialDepositId.HasValue)
            {
                transaction = Repo.GetTransactionById(result.InitialDepositId.Value);
            }
            return new BankAccountDetailVM(account, result, transaction);
        }

        #endregion Saves

        #region Deactivation

        internal DateTime DeactivateAccount(int accountId)
        {
            var account = GetGenericAccountDetails(accountId);
            switch (account.AccountType)
            {
                case Constants.AccountType.BankAccount:
                    Repo.DeactivateDepositAccount(accountId);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return Repo.DeactivateAccount(accountId).DateTime_Deactivated.Value;
        }

        #endregion Deactivation

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

    }
}
