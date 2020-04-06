using System.Collections.Generic;
using Budgetor.Helpers;

namespace Budgetor.Models
{
    public class ManageBankAccountVM : BindableBase
    {
        private BankAccountDetailVM _OGAccount;
        public BankAccountDetailVM OGAccount
        {
            get { return _OGAccount; }
            set
            {
                _OGAccount = value;
                Account = new BankAccountDetailVM()
                {
                    AccountId = value.AccountId,
                    AccountName = value.AccountName,
                    DateTime_Created = value.DateTime_Created,
                    DateTime_Deactivated = value.DateTime_Deactivated,
                    DepositAccountId = value.DepositAccountId,
                    InitialBalance = value.InitialBalance,
                    InitialDepositId = value.InitialDepositId,
                    IsActiveCashAccount = value.IsActiveCashAccount,
                    IsDefault = value.IsDefault,
                    Notes = value.Notes
                };
            }
        }
        private BankAccountDetailVM _Account;
        public BankAccountDetailVM Account
        {
            get => _Account;
            set
            {
                if (value != _Account)
                {
                    _Account = value;
                    RaisePropertyChanged("Account");
                }
            }
        }

        public bool IsDirty => IsAddMode || (Account != OGAccount);
        public bool IsEditMode { get; set; }
        public bool IsAddMode
        {
            get { return !IsEditMode; }
            set { IsEditMode = !value; }
        }

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
