using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Repo.Models
{
    public class DepositAccount_DetailView
    {
        public int DepositAccountId { get; set; }

        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public string Notes { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool IsSystem { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public decimal Balance { get; set; }

        public int? InitialDepositId { get; set; }

        public int? InitialBalance { get; set; }

        public int? InitialDepositAccountId { get; set; }
        
    }
}
