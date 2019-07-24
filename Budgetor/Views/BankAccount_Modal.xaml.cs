using System;
using System.Windows;
using Budgetor.Constants;
using Budgetor.Helpers;
using Budgetor.Helpers.Delegates;
using Budgetor.Models;
using Budgetor.Overminds;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BankAccount_Modal.xaml
    /// </summary>
    public partial class BankAccount_Modal : Window
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

        public bool IsDirty => IsAddMode || (Account != OGAccount);

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
                    InitialDepositId = value.InitialDepositId,
                    IsActiveCashAccount = value.IsActiveCashAccount,
                    IsDefault = value.IsDefault,
                    Notes = value.Notes
                };
            }
        }
        public BankAccountDetailVM Account { get; set; }

        private ModalCloseDelegate _OnClose;
        private OpenTransactionModalDelegate _OpenTransactionModal;

        #endregion Properties

        #region Constructors

        public BankAccount_Modal(
            ManageBankAccountVM initialVM, 
            AccountsOM accountOverMind, 
            TransactionsOM transactionsOverMind,
            OpenTransactionModalDelegate openTransactionModal,
            ModalCloseDelegate onClose = null
        )
        {
            _OpenTransactionModal = openTransactionModal;
            _OnClose = onClose;
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
            Account.DateTime_Deactivated = DateTime.UtcNow;
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsAddMode)
                {
                    // Store Initial Deposit info or will be lost on account save
                    decimal? amount = Account.InitialBalance;

                    // Save Account without transaction to get an AccountId
                    OGAccount = accountsOM.SaveAccount(Account);

                    // Create Initial Transaction and update the account with it's info
                    if (amount.HasValue)
                    {
                        InitialDeposit = new TransactionSaveInfo()
                        {
                            Amount = amount.Value,
                            DateTime_Occurred = Account.DateTime_Created,
                            FromAccount = vm.SelectedFromAccount,
                            IsConfirmed = true,
                            IsUserCreated = false,
                            Title = "Initial Deposit",
                            OccerrenceAccount = null,
                            ToAccount = Account.AccountId,
                            TransactionType = TransactionType.Deposit,
                            Notes = $"Initial Deposit for {Account.AccountName} {Constants.Accounts.GetDisplay(Account.AccountType)}"
                        };

                        TransactionDetailBase savedTransaction = transactionsOM.SaveTransaction(InitialDeposit);

                        Account.InitialBalance = savedTransaction.Amount;
                        Account.InitialDepositAccountId = savedTransaction.FromAccount.AccountId;
                        Account.InitialDepositId = savedTransaction.TransactionId;

                        OGAccount = accountsOM.SaveAccount(Account);
                    }
                }
                else
                {
                    if (IsDirty)
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
            _OpenTransactionModal(Account.InitialDepositId);
        }

        private void OnModalClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnModalClose();
        }

        #endregion Form Calls

        #region Private Methods

        private void OnModalClose()
        {
            _OnClose?.Invoke(IsDirty);
        }

        #endregion Private Methods
    }
}
