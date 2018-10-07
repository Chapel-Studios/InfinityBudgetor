using Budgetor.Models;
using System;
using System.Collections.Generic;

namespace Budgetor.Models
{
    public class AccountsTabVM : BindableBase
    {
        private List<BankAccountListVM> _BankAccounts;
        public List<BankAccountListVM> BankAccounts
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

        private List<IncomeSourceListVM> _IncomeSources;
        public List<IncomeSourceListVM> IncomeSources
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
