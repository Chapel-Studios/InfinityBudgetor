using Budgetor.Helpers.Extensions;
using Budgetor.Repo.Models;
using System;
using System.Text.RegularExpressions;

namespace Budgetor.Models.Accounts
{
    public class BankAccountDetailVM : AccountDetailVM
    {
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public int? InitialDepositId { get; set; }

        public int? InitialDepositAccountId { get; set; }

        private decimal? _InitialBalance;
        public decimal? InitialBalance
        {
            get
            {
                return _InitialBalance;
            }
            set
            {
                _InitialBalance = value;
            }
        }

        public string InitialBalance_Display
        {
            get
            {
                if (InitialBalance.HasValue)
                    return this.InitialBalance.Value.GetDisplayAmountText();
                else
                    return DispayExtensions.GetDisplayAmountText(0);
            }
            set
            {
                value = Regex.Replace(value, "[^0-9.]", String.Empty);
                var b = decimal.TryParse(value, out decimal d);
                if (b)
                {
                    this.InitialBalance = d == 0 ? (decimal?)null : d;
                }
            }
        }

        public BankAccountDetailVM() : base(Constants.AccountType.BankAccount)
        {
            DateTime_Created = DateTime.UtcNow;
        }

        public BankAccountDetailVM(DepositAccount_DetailView repoModel) : base(Constants.AccountType.BankAccount)
        {
            DepositAccountId = repoModel.DepositAccountId;
            IsDefault = repoModel.IsDefault;
            IsActiveCashAccount = repoModel.IsActiveCashAccount;
            InitialDepositId = repoModel.InitialDepositId;
            InitialDepositAccountId = repoModel.InitialDepositAccountId;
            InitialBalance = repoModel.InitialBalance;
            Notes = repoModel.Notes;
            AccountId = repoModel.AccountId;
            AccountName = repoModel.AccountName;
            DateTime_Created = repoModel.DateTime_Created;
            DateTime_Deactivated = repoModel.DateTime_Deactivated;
        }

        public BankAccountDetailVM(AccountDetailVM baseAccount, DepositAccount depositAccount, Transaction transaction) : base(baseAccount.AccountType)
        {
            AccountName = baseAccount.AccountName;
            Notes = baseAccount.Notes;
            DepositAccountId = depositAccount.LocalId;
            AccountId = baseAccount.AccountId;
            DateTime_Created = baseAccount.DateTime_Created;
            DateTime_Deactivated = baseAccount.DateTime_Deactivated;
            IsDefault = depositAccount.IsDefault;
            IsActiveCashAccount = depositAccount.IsActiveCashAccount;
            InitialDepositId = depositAccount.InitialDepositId;
            InitialBalance = transaction?.Amount;
            InitialDepositAccountId = transaction?.FromAccount;
        }
    }
}
