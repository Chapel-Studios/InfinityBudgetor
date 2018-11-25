using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class TransactionSaveModel
    {
        public readonly int TransactionId;

        public string Title { get; set; }

        public Constants.Transactions.TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public int ToAccount { get; set; }

        public int FromAccount { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public int? OccerrenceAccount { get; set; }

        public TransactionSaveModel()
        {
            TransactionId = 0;
        }

        public TransactionSaveModel(int id)
        {
            TransactionId = id;
        }
    }
}
