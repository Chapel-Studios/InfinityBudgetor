using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetor.Constants;
using Budgetor.Repo.Models;
using Budgetor.Helpers;

namespace Budgetor.Models
{
    public class TransactionDetailBase : ITransactionDetail
    {
        public string Title { get; set; }

        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal? Amount { get; set; }

        public string Amount_Display
        {
            get
            {
                if (Amount.HasValue)
                    return DispayFormatHelper.GetDisplayAmountText(this.Amount.Value);
                else
                    return DispayFormatHelper.GetDisplayAmountText(0);
            }
            set
            {
                var b = decimal.TryParse(value, out decimal d);
                if (b)
                    this.Amount = d;
            }
        }

        public AccountBasicInfo ToAccount { get; set; }

        public AccountBasicInfo FromAccount { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public AccountBasicInfo OccerrenceAccount { get; set; }

        public string Notes { get; set; }

        public TransactionDetailBase()
        {

        }

        public TransactionDetailBase(TransactionType transactionType)
        {
            TransactionType = transactionType;
        }

        public TransactionDetailBase(Transaction repoTransaction, AccountBasicInfo toAccount, AccountBasicInfo fromAccount, AccountBasicInfo occerrenceAccount)
        {
            Title = repoTransaction.Title;
            TransactionType = Constants.Transactions.GetDisplayByTypeName(repoTransaction.TransactionType).EnumOption;
            Amount = repoTransaction.Amount;
            ToAccount = toAccount;
            FromAccount = fromAccount;
            DateTime_Created = repoTransaction.DateTime_Created;
            DateTime_Occurred = repoTransaction.DateTime_Occurred;
            IsUserCreated = repoTransaction.IsUserCreated;
            IsConfirmed = repoTransaction.IsConfirmed;
            OccerrenceAccount = occerrenceAccount;
            Notes = repoTransaction.Notes;
            TransactionId = repoTransaction.LocalId;
        }
    }
}
