using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budgetor.Constants.Accounts;
using static Budgetor.Constants.Transactions;

namespace Budgetor.Models
{
    public class TransactionBase
    {
        public string Title { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public AccountBasicInfo ToAccount { get; set; }

        public AccountBasicInfo FromAccount { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsOccurrence { get; set; }

        public AccountBasicInfo OccerrenceAccount { get; set; }
    }
}
