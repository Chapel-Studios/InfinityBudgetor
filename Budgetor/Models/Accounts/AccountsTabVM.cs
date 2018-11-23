using System.Collections.ObjectModel;

namespace Budgetor.Models
{
    public class AccountsTabVM : BindableBase
    {
        private ObservableCollection<BankAccountsListItemVM> _BankAccounts;
        public ObservableCollection<BankAccountsListItemVM> BankAccounts
        {
            get
            {
                return _BankAccounts;
            }
            set
            {
                if (value != _BankAccounts)
                {
                    _BankAccounts = value;
                    RaisePropertyChanged("BankAccounts");
                }
            }
        }

        private ObservableCollection<IncomeSourceListVM> _IncomeSources;
        public ObservableCollection<IncomeSourceListVM> IncomeSources
        {
            get
            {
                return _IncomeSources;
            }
            set
            {
                if (value != _IncomeSources)
                {
                    _IncomeSources = value;
                    RaisePropertyChanged("IncomeSources");
                }
            }
        }
    }
}
