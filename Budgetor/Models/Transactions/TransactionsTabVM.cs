using System.Collections.ObjectModel;

namespace Budgetor.Models
{
    public class TransactionsTabVM :  BindableBase
    {
        private ObservableCollection<TransactionsListItemVM> _Transactions;

        public TransactionsTabVM(ObservableCollection<TransactionsListItemVM> transactions)
        {
            Transactions = transactions;
        }

        protected ObservableCollection<TransactionsListItemVM> Transactions
        {
            set
            {
                if (value != _Transactions)
                {
                    _Transactions = value;
                    RaisePropertyChanged("Transactions");
                }
            }
        }
        public ObservableCollection<TransactionsListItemVM> DisplayTransactions
        {
            get
            {
                return _Transactions;
            }
        }
    }
}
