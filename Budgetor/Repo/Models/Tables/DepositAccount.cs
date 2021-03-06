namespace Budgetor.Repo.Models
{
    using Budgetor.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepositAccount")]
    public partial class DepositAccount
    {
        [Key]
        public int LocalId { get; set; }

        public int AccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public decimal Balance { get; set; }

        public int? InitialDepositId { get; set; }

        public DepositAccount()
        {

        }

        public DepositAccount(BankAccountDetailVM accountDetailVM)
        {
            LocalId = accountDetailVM.DepositAccountId;
            AccountId = accountDetailVM.AccountId;
            InitialDepositId = accountDetailVM.InitialDepositId;
            IsActiveCashAccount = accountDetailVM.IsActiveCashAccount;
            IsDefault = accountDetailVM.IsDefault;
        }

    }
}
