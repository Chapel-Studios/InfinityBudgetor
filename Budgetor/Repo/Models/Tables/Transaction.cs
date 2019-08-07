using Budgetor.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budgetor.Repo.Models
{
    public partial class Transaction
    {
        [Key]
        public int LocalId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public decimal Amount { get; set; }

        public int? ToAccount { get; set; }

        public int FromAccount { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime DateTime_Occurred { get; set; }

        [Required]
        [StringLength(10)]
        public string TransactionType { get; set; }

        public bool IsUserCreated { get; set; }

        public bool IsConfirmed { get; set; }

        public int? OccerrenceAccount { get; set; }

        public string Notes { get; set; }

        public Transaction()
        {

        }

        public Transaction(TransactionSaveInfo saveModel)
        {
            LocalId = saveModel.TransactionId;
            Title = saveModel.Title;
            Amount = saveModel.Amount;
            ToAccount = saveModel.ToAccount;
            FromAccount = saveModel.FromAccount;
            DateTime_Occurred = saveModel.DateTime_Occurred;
            TransactionType = Constants.Transactions.GetDisplay(saveModel.TransactionType).TypeName;
            IsUserCreated = saveModel.IsUserCreated;
            IsConfirmed = saveModel.IsConfirmed;
            OccerrenceAccount = saveModel.OccerrenceAccount;
            Notes = saveModel.Notes;
        }
    }
}
