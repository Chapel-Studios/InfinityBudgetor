using System.Collections.Generic;
using Budgetor.Helpers;

namespace Budgetor.Models
{
    public class ManageBankAccountVM : BindableBase
    {
        public BankAccountDetailVM Account { get; set; }

        public bool IsEditMode { get; set; }

        public List<AccountComboBoxItem> FromAccounts { get; set; }

        private int? _SelectedFromAccount;
        public int SelectedFromAccount
        {
            get
            {
                if (!_SelectedFromAccount.HasValue)
                {
                    _SelectedFromAccount = FromAccounts.GetDefaultAccount();
                }
                return _SelectedFromAccount ?? 0;
            }
            set
            {
                _SelectedFromAccount = value;
            }
        }

        public ManageBankAccountVM()
        {
            FromAccounts = new List<AccountComboBoxItem>();
        }
    }
}
