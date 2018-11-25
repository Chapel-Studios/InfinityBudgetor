using Budgetor.Controls;
using Budgetor.Models;
using Budgetor.Overminds;
using System.Windows;
namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BudgetorMain.xaml
    /// </summary>
    public partial class BudgetorMain : Window
    {
        #region Properties

        private AccountsOM _AccountsOM;
        public AccountsOM AccountsOM
        {
            get
            {
                if (_AccountsOM == null)
                {
                    _AccountsOM = new AccountsOM();
                }

                return _AccountsOM;
            }
        }

        private TransactionsOM _TransactionsOM;
        public TransactionsOM TransactionsOM
        {
            get
            {
                if (_TransactionsOM == null)
                {
                    _TransactionsOM = new TransactionsOM();
                }

                return _TransactionsOM;
            }
        }

        public AccountsTabVM AccountsTabVM;


        #endregion Properties

        #region Constructors

        public BudgetorMain()
        {
            InitializeComponent();

            AccountsTabVM = AccountsOM.GetAcountsTabVM();
            AccountsTab.DataContext = AccountsTabVM;
        }

        #endregion Constructors

        #region Accounts Tab Calls

        private void EditBankAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as EditButton;
            EditBankAccount(button.AccountId);
        }

        private void AddBankAccount_Button_Click(object sender, RoutedEventArgs e)
        {
            EditBankAccount(null);
        }

        private void AddIncSource_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as EditButton;
            EditBankAccount(button.AccountId);
        }

        #endregion Accounts Tab Calls

        #region Transactions Tab Calls

        private void AddNewTransaction_Button_Click(object sender, RoutedEventArgs e)
        {
            EditTransaction(null);
        }

        #endregion Transactions Tab Calls

        #region Private Methods

        private void EditBankAccount(int? id)
        {
            var editor = new BankAccount_Form(AccountsOM.GetEditBankAccountVM(id), AccountsOM);
            editor.Show();
        }

        private void EditIncSource(int? id)
        {
            //var editor = new BankAccount_Form(AccountFactory.GetEditBankAccountVM(id), AccountFactory);
            //editor.Show();
        }

        private void EditTransaction(int? id)
        {
            var editor = new Transaction_Modal(TransactionsOM.GetTransactionModalVM(id), TransactionsOM);
            editor.Show();
        }

        #endregion Private Methods
    }
}
