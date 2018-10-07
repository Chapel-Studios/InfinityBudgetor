
using Budgetor.Overminds;
using Budgetor.Models;

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
                BankAccounts = accountOverMind.GetBankAccountsList(),
                IncomeSources = accountOverMind.GetIncomeSourcesList()
            };
        }
    }
}
