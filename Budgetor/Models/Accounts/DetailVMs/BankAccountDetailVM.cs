﻿using Budgetor.Helpers;
using Budgetor.Models;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class BankAccountDetailVM : AccountDetailVM
    {
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public int? InitialDepositId { get; set; }

        public int? InitialDepositAccountId { get; set; }

        public decimal? InitialBalance { get; set; }

        public string InitialBalance_Display
        {
            get
            {
                if (InitialBalance.HasValue)
                    return DispayFormatHelper.GetDisplayAmountText(this.InitialBalance.Value);
                else
                    return DispayFormatHelper.GetDisplayAmountText(0);
            }
            set
            {
                var b = decimal.TryParse(value, out decimal d);
                if (b)
                    this.InitialBalance = d;
            }
        }

        public BankAccountDetailVM() : base(Constants.Accounts.AccountType.BankAccount)
        {
        }

        public BankAccountDetailVM(DepositAccount_DetailView repoModel) : base(Constants.Accounts.AccountType.BankAccount)
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
