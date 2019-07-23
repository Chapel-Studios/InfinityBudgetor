using Budgetor.Models.Contracts;
using System;

namespace Budgetor.Models
{
    public class TransactionSaveInfo
    {
        #region Properties

        public readonly int TransactionId;

        public string Title { get; set; }

        public string Notes { get; set; }

        public Constants.TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }

        public int? ToAccount { get; set; }

        public int FromAccount { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public int? OccerrenceAccount { get; set; }

        #endregion Properties

        #region Constructors

        public TransactionSaveInfo()
        {
            TransactionId = 0;
        }

        public TransactionSaveInfo(int id)
        {
            TransactionId = id;
        }

        public TransactionSaveInfo(ITransactionDetail transactionDetail)
        {
            TransactionId = transactionDetail.TransactionId;
            MapFromITransactionDetail(transactionDetail);
        }

        public TransactionSaveInfo(TransactionModalVM modalVM)
        {
            TransactionId = modalVM.Transaction.TransactionId;
            MapFromITransactionDetail(modalVM.Transaction);
            
            FromAccount = modalVM.SelectedFromAccount;
            ToAccount = modalVM.SelectedToAccount == 0 ? (int?)null : modalVM.SelectedToAccount;
        }

        #endregion Constructors

        #region Private Methods

        private void MapFromITransactionDetail(ITransactionDetail transactionDetail)
        {
            Title = transactionDetail.Title;
            TransactionType = transactionDetail.TransactionType;
            Amount = transactionDetail.Amount;
            ToAccount = transactionDetail.FromAccount?.AccountId ?? 0;
            DateTime_Occurred = transactionDetail.DateTime_Occurred;
            IsUserCreated = transactionDetail.IsUserCreated;
            IsConfirmed = transactionDetail.IsConfirmed;
            OccerrenceAccount = transactionDetail.OccerrenceAccount?.AccountId;
            Notes = transactionDetail.Notes;
            ToAccount = transactionDetail.ToAccount?.AccountId;
            FromAccount = transactionDetail.FromAccount?.AccountId ?? 0;
        }

        #endregion Private Methods
    }
}
