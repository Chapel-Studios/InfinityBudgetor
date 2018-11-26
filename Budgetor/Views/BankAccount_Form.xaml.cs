using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Budgetor.Constants;
using Budgetor.Models;
using Budgetor.Overminds;
using Budgetor.Helpers.Extensions;

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
        private TransactionSaveModel InitialDeposit { get; set; }

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

        private int? _SelectedFromAccount;
        public int SelectedFromAccount
        {
            get
            {
                if (!_SelectedFromAccount.HasValue)
                {
                    if (vm != null)
                    {
                        _SelectedFromAccount = vm.FromAccounts.GetDefaultAccount();
                    }
                    else
                    {
                        _SelectedFromAccount = 1;
                    }
                }
                return _SelectedFromAccount.Value;
            }
            set
            {
                _SelectedFromAccount = value;
            }
        }

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

            FromAccount_ComboBox.BindToList(initialVM.FromAccounts, SelectedFromAccount);
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
            var amount = Account.InitialBalance.Value;

            if (Account != OGAccount)
            {
                OGAccount = accountsOM.SaveAccount(Account);
            }
            if (IsAddMode)
            {
                InitialDeposit = new TransactionSaveModel()
                {
                    Amount = amount,
                    DateTime_Occurred = Account.DateTime_Created,
                    FromAccount = SelectedFromAccount,
                    IsConfirmed = true,
                    IsUserCreated = false,
                    Title = "Default Cash Account Initial Deposit",
                    OccerrenceAccount = null,
                    ToAccount = Account.AccountId,
                    TransactionType = TransactionType.Deposit
                };
                transactionsOM.SaveTransaction(InitialDeposit);
            }
            this.Close();
        }

        private void EditTransaction_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion Form Calls

        #region Private Methods

        

        #endregion Private Methods
    }
}
