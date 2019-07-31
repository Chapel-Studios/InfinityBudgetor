using System;
using System.ComponentModel.DataAnnotations;

namespace Budgetor.Repo.Models
{
    public class BankAccountsListView
    {
        [Key]
        public int DepositAccountId { get; set; }

        public int AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        //todo: need to figure out how we're getting this
        //public decimal Balance { get; set; }
    }
}
