using Budgetor.Constants;
using Budgetor.Helpers.Extensions;

namespace Budgetor.Models.Transactions
{
    public class TransactionsListItemVM : TransactionDetailBase
    {
        #region Constructors

        public TransactionsListItemVM(TransactionType transactionType) : base(transactionType)
        {

        }

        public TransactionsListItemVM(TransactionDetailBase detailBase) : base(detailBase.TransactionType)
        {
            Amount = detailBase.Amount;
            DateTime_Created = detailBase.DateTime_Created;
            DateTime_Occurred = detailBase.DateTime_Occurred;
            Title = detailBase.Title;
            TransactionId = detailBase.TransactionId;
            TransactionType = detailBase.TransactionType;
            Amount = detailBase.Amount;
            ToAccount = detailBase.ToAccount;
            FromAccount = detailBase.FromAccount;
            IsUserCreated = detailBase.IsUserCreated;
            IsConfirmed = detailBase.IsConfirmed;
            OccerrenceAccount = detailBase.OccerrenceAccount;
            Notes = detailBase.Notes;
        }

        #endregion Constructors

        #region Properties

        public string Display_DateTime_Occurred { get => DateTime_Occurred.GetDisplayDate(); }

        public string Display_TransactionType { get => Constants.Transactions.GetDisplay(this.TransactionType).Display; }

        public string Display_Amount
        {
            get
            {
                return Amount.GetDisplayAmountText();
            }
        }

        public string Display_Account
        {
            get
            {
                switch (TransactionType)
                {
                    case TransactionType.Deposit:
                        return $"{FromAccount?.AccountName ?? ""} => {ToAccount.AccountName ?? ""}";
                    case TransactionType.Purchase:
                        return $"{FromAccount?.AccountName ?? "Not Tracked"}";
                    default:
                        return "Transaction Error";
                }
            }
        }

        #endregion Properties
    }
}
