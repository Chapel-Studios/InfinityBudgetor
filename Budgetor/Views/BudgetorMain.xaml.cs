using Budgetor.Controls;
using Budgetor.Models;
using Budgetor.Overminds;
using System;
using System.Windows;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BudgetorMain.xaml
    /// </summary>
    public partial class BudgetorMain : Window
    {
        #region Properties

        private readonly Repo.Repository _Repo;

        private AccountsOM _AccountsOM;
        public AccountsOM AccountsOM
        {
            get
            {
                if (_AccountsOM == null)
                {
                    _AccountsOM = new AccountsOM(_Repo);
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
                    _TransactionsOM = new TransactionsOM(_Repo);
                }

                return _TransactionsOM;
            }
        }

        public AccountsTabVM AccountsTabVM;
        public TransactionsTabVM TransactionsTabVM;


        #endregion Properties

        #region Constructors

        public BudgetorMain()
        {
            InitializeComponent();

            _Repo = new Repo.Repository();

            UpdateAccountsTab();
            UpdateTransactionsTab();

            AccountsTab.DataContext = AccountsTabVM;

            TransactionsTab.DataContext = TransactionsTabVM;
        }

        #endregion Constructors

        #region Accounts Tab Calls

        private void EditBankAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as EditButton;
                EditBankAccount(button.ContextualId);
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        private void AddBankAccount_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditBankAccount(null);
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        private void AddIncSource_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as EditButton;
                EditBankAccount(button.ContextualId);
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        private void EditIncSource_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as EditButton;
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        #endregion Accounts Tab Calls

        #region Transactions Tab Calls

        private void AddNewTransaction_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EditTransaction(null);
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        private void EditTransaction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as EditButton;
                EditTransaction(button.ContextualId);
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        #endregion Transactions Tab Calls

        #region Private Methods

        private void EditBankAccount(int? id)
        {
            var editor = new BankAccount_Form(AccountsOM.GetEditBankAccountVM(id), AccountsOM, TransactionsOM, UpdateCallback);
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

        private void UpdateCallback(bool isUpdateRequired = true)
        {
            if (isUpdateRequired)
            {
                UpdateAccountsTab();
                UpdateTransactionsTab();
            }
        }

        private void UpdateAccountsTab()
        {
            AccountsTabVM = AccountsOM.GetAcountsTabVM();
        }

        private void UpdateTransactionsTab()
        {
            TransactionsTabVM = TransactionsOM.GetTransactionsTabVM();
        }

        #endregion Private Methods
    }
}
