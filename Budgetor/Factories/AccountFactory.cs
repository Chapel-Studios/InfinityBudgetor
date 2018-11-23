
using Budgetor.Overminds;
using Budgetor.Models;
using System.Collections.ObjectModel;

namespace Budgetor.Factories
{
    public class AccountFactory
    {
        private AccountsOM _accountOverMind;
        protected AccountsOM AccountOverMind
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
                BankAccounts = new ObservableCollection<BankAccountsListItemVM>(AccountOverMind.GetBankAccountsList()),
                IncomeSources = new ObservableCollection<IncomeSourceListVM>(AccountOverMind.GetIncomeSourcesList())
            };
        }

        public EditBankAccountVM GetEditBankAccountVM(int? id)
        {
            BankAccountDetailVM bankAccount;
            if (id.HasValue)
            {
                bankAccount = AccountOverMind.GetBankAccountById(id.Value);
            }
            else
            {
                bankAccount = new BankAccountDetailVM();
            }

            return new EditBankAccountVM()
            {
                BankAccount = bankAccount
            };
        }
    }
}
