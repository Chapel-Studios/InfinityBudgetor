
using Budgetor.Overminds;
using Budgetor.Models;
using System.Collections.ObjectModel;

namespace Budgetor.Factories
{
    public class AccountFactory
    {
        private AccountsOM _accountOverMind;
        private AccountsOM accountOverMind
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

        public AccountsTabVM GetAcountsTabVM()
        {
            return new AccountsTabVM()
            {
                BankAccounts = new ObservableCollection<BankAccountListVM>(accountOverMind.GetBankAccountsList()),
                IncomeSources = new ObservableCollection<IncomeSourceListVM>(accountOverMind.GetIncomeSourcesList())
            };
        }
    }
}
