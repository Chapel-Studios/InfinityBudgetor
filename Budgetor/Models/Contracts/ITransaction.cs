using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Contracts
{
    interface ITransactionDetail
    {
        string Title { get; set; }

        Constants.Transactions.TransactionType TransactionType { get; }

        decimal Amount { get; set; }

        AccountBasicInfo ToAccount { get; set; }

        AccountBasicInfo FromAccount { get; set; }

        DateTime DateTime_Created { get; set; }

        DateTime DateTime_Occurred { get; set; }

        bool IsUserCreated { get; set; }

        bool IsConfirmed { get; set; }

        AccountBasicInfo OccerrenceAccount { get; set; }

    }
}
