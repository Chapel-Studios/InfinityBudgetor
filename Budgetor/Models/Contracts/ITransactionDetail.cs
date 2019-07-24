using Budgetor.Constants;
using System;

namespace Budgetor.Models.Contracts
{
    public interface ITransactionDetail
    {
        string Title { get; set; }

        int TransactionId { get; set; }

        TransactionType TransactionType { get; set; }

        decimal Amount { get; set; }

        AccountBasicInfo ToAccount { get; set; }

        AccountBasicInfo FromAccount { get; set; }

        DateTime DateTime_Created { get; set; }

        DateTime DateTime_Occurred { get; set; }

        bool IsUserCreated { get; set; }

        bool IsConfirmed { get; set; }

        AccountBasicInfo OccerrenceAccount { get; set; }

        string Notes { get; set; }
    }
}
