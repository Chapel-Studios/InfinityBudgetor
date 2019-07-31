namespace Budgetor.Models.Transactions
{
    class DepositDetail : TransactionDetailBase
    {
        public DepositDetail() : base(Constants.TransactionType.Deposit)
        {

        }
    }
}
