using System;
using System.Windows;
using Budgetor.Constants;
using Budgetor.Helpers;
using Budgetor.Models;
using Budgetor.Overminds;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BankAccount_Form.xaml
    /// </summary>
    public partial class BankAccount_Form : Window
    {
        #region Properties

        private AccountsOM accountsOM;
        private TransactionsOM transactionsOM;

        private ManageBankAccountVM vm;

        private bool ShowCreationDateWarning { get; set; }
        public bool NeedResponse { get; set; }
        private bool DidUserCancel { get; set; }
        private TransactionSaveInfo InitialDeposit { get; set; }

        public bool IsEditMode { get; set; }
        public bool IsAddMode
        {
            get { return !IsEditMode; }
            set { IsEditMode = !value; }
        }

        private BankAccountDetailVM _OGAccount;
        public BankAccountDetailVM OGAccount
        {
            get { return _OGAccount; }
            set
            {
                _OGAccount = value;
                Account = new BankAccountDetailVM()
                {
                    AccountId = value.AccountId,
                    AccountName = value.AccountName,
                    DateTime_Created = value.DateTime_Created,
                    DateTime_Deactivated = value.DateTime_Deactivated,
                    DepositAccountId = value.DepositAccountId,
                    InitialBalance = value.InitialBalance,
                    InitialBalance_Display = value.InitialBalance_Display,
                    InitialDepositId = value.InitialDepositId,
                    IsActiveCashAccount = value.IsActiveCashAccount,
                    IsDefault = value.IsDefault,
                    Notes = value.Notes
                };
            }
        }
        public BankAccountDetailVM Account { get; set; }

        #endregion Properties

        #region Constructors

        public BankAccount_Form(ManageBankAccountVM initialVM, AccountsOM accountOverMind, TransactionsOM transactionsOverMind)
        {
            accountsOM = accountOverMind;
            transactionsOM = transactionsOverMind;
            vm = initialVM;
            IsEditMode = initialVM.IsEditMode;
            OGAccount = initialVM.Account;

            InitializeComponent();

            Title = vm.IsEditMode ? vm.Account.AccountName : "Add a new bank account";

            VMHandle.DataContext = vm;
            BankAccount_Grid.DataContext = Account;

            FromAccount_ComboBox.BindToList(vm, "FromAccounts", "SelectedFromAccount");
        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Deactivate_Button_Click(object sender, RoutedEventArgs e)
        {
            accountsOM.DeactivateAccount(vm.Account.AccountId);
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsAddMode)
                {
                    var amount = Account.InitialBalance.HasValue ? Account.InitialBalance.Value : 0.0M;
                    InitialDeposit = new TransactionSaveInfo()
                    {
                        Amount = amount,
                        DateTime_Occurred = Account.DateTime_Created,
                        FromAccount = vm.SelectedFromAccount,
                        IsConfirmed = true,
                        IsUserCreated = false,
                        Title = "Default Cash Account Initial Deposit",
                        OccerrenceAccount = null,
                        ToAccount = Account.AccountId,
                        TransactionType = TransactionType.Deposit
                    };
                    if (InitialDeposit.Amount != 0)
                        transactionsOM.SaveTransaction(InitialDeposit);
                }
                else
                {
                    if (Account != OGAccount)
                    {
                        OGAccount = accountsOM.SaveAccount(Account);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
        }

        private void EditTransaction_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion Form Calls

        #region Private Methods

        

        #endregion Private Methods
    }
}
