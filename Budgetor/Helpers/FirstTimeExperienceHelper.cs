using Budgetor.Constants;
using Budgetor.Models;
using Budgetor.Models.Accounts;
using Budgetor.Overminds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Budgetor.Helpers
{
    public class FirstTimeExperienceHelper : IDisposable
    {
        private AccountsOM _accountOverMind;
        public AccountsOM AccountOverMind
        {
            get
            {
                if (_accountOverMind == null)
                {
                    _accountOverMind = new AccountsOM();
                }
                return _accountOverMind;
            }
        }

        private readonly bool hasRan;

        public FirstTimeExperienceHelper()
        {
            hasRan = Budgetor.Properties.Settings.Default.DateTime_FirstRun != DateTime.MinValue;

            //todo: remove Dev testing option
            //if (hasRan)
            //{
            //    var force = MessageBox.Show("Force First-Run Init?", "Dev", MessageBoxButton.YesNo);
            //    if (force == MessageBoxResult.Yes)
            //    {
            //        hasRan = false;
            //    }
            //}
        }

        public void FirstRunInit()
        {
            if (!hasRan)
            {
                var depAcct = CreateDoNotTrackIncomeSource();
                var cash = CreateDefaultCashAccount();
                depAcct.DefaultToAccountId = cash.DepositAccountId;
                AccountOverMind.SaveAccount(depAcct);

                Budgetor.Properties.Settings.Default.DateTime_FirstRun = DateTime.UtcNow;
                Budgetor.Properties.Settings.Default.SystemIncomeSource = depAcct.IncomeSourceId;
                Budgetor.Properties.Settings.Default.Save();
            }
        }



        private IncomeSourceDetailVM CreateDoNotTrackIncomeSource()
        {
            IncomeSourceDetailVM nullSource = new IncomeSourceDetailVM()
            {
                AccountName = AccountTypesConstants.DefaultIncomeSourceAccountName,
                Schedule = null,
                DefaultToAccountId = null,
                ExpectedAmount = 0,
                TotalFromSource = 0,
                Notes = "Default account placeholder for when income source data is not tracked seperately. Info kept for checks and balances",
            };

            nullSource = AccountOverMind.SaveAccount(nullSource);
            if (nullSource.IncomeSourceId == 0)
            {
                throw new Exception("System worked but not saving the null source....");
            }
            AccountOverMind.SetAccountToSystem(nullSource.AccountId);
            return nullSource;
        }

        private BankAccountDetailVM CreateDefaultCashAccount()
        {
            BankAccountDetailVM cash = null;

            var shouldCreate = MessageBox.Show("Would you like to create an account to track your cash?", "Welcome", MessageBoxButton.YesNo);
            if (shouldCreate == MessageBoxResult.Cancel || shouldCreate == MessageBoxResult.No)
            {
                MessageBox.Show("If you change your mind you'll have to manually make a cash account later.",
                                button: MessageBoxButton.OK,
                                caption: MiscConstants.AppName);
            }
            else if (shouldCreate == MessageBoxResult.Yes)
            {
                cash = new BankAccountDetailVM()
                {
                    AccountName = AccountTypesConstants.DefaultCashAccountName,
                    InitialDepositId = null,
                    IsActiveCashAccount = true,
                    IsDefault = true,
                    Notes = "Physical Money"
                };
                cash = AccountOverMind.SaveAccount(cash);
            }
            else
            {
                throw new Exception("WTF! options shouldn't exist!");
            }

            return cash;
        }

        #region IDisposable Implementation
        private IntPtr handle;
        private Component component = new Component();
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        component.Dispose();
                    }
                }

                CloseHandle(handle);
                handle = IntPtr.Zero;

                disposed = true;
            }
        }

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        #endregion IDisposable Implementation
    }
}
