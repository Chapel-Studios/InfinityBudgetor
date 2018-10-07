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
    public class FirstTimeInitHelper : IDisposable
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

        private AccountDetailVM sysAccount;

        public FirstTimeInitHelper()
        {
            
        }

        public void CreateDoNotTrackIncomeSource()
        {
            sysAccount = new AccountDetailVM()
            {
                AccountName = AccountTypesConstants.SystemAccountName,
                Notes = "Default account used by the system when no account exists."
            };

            sysAccount = AccountOverMind.SaveAccount(sysAccount);

            if (sysAccount.LocalId == 0)
            {
                throw new Exception("Save System Account failed... wtf?");
            }

            IncomeSourceDetailVM nullSource = new IncomeSourceDetailVM()
            {
                AccountName = AccountTypesConstants.DefaultIncomeSourceAccountName,
                Schedule = null,
                DefaultToAccount = sysAccount.LocalId,
                ExpectedAmount = 0,
                TotalFromSource = 0,
                Notes = "Default account placeholder for when income source data is not tracked seperately. Info kept for checks and balances"
            };

            nullSource = AccountOverMind.SaveAccount(nullSource);
            if (nullSource.LocalId == 0)
            {
                throw new Exception("System worked but not saving the null source....");
            }
        }

        public void CreateDefaultCashAccount()
        {
            var shouldCreate = MessageBox.Show("Would you like to create an account to track your cash?", "Welcome", MessageBoxButton.YesNo);
            if (shouldCreate == MessageBoxResult.Cancel || shouldCreate == MessageBoxResult.No)
            {
                MessageBox.Show("If you change your mind you'll have to manually make a cash account later.",
                                button: MessageBoxButton.OK,
                                caption: MiscConstants.AppName);
            }
            else if (shouldCreate == MessageBoxResult.Yes)
            {
                //do stuff
            }
            else
            {
                throw new Exception("WTF! options shouldn't exist!");
            }

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
