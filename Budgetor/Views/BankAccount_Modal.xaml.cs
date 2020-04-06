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

            InitializeComponent();

            Title = vm.IsEditMode ? vm.Account.AccountName : "Add a new bank account";

            VMHandle.DataContext = vm;

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
            vm.Account.DateTime_Deactivated = DateTime.UtcNow;
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vm.IsAddMode)
                {
                    // Store Initial Deposit info or will be lost on account save
                    decimal? amount = vm.Account.InitialBalance;

                    // Save Account without transaction to get an AccountId
                    vm.OGAccount = accountsOM.SaveAccount(vm.Account);

                    // Create Initial Transaction and update the account with it's info
                    if (amount.HasValue)
                    {
                        InitialDeposit = new TransactionSaveInfo()
                        {
                            Amount = amount.Value,
                            DateTime_Occurred = vm.Account.DateTime_Created,
                            FromAccount = vm.SelectedFromAccount,
                            IsConfirmed = true,
                            IsUserCreated = false,
                            Title = "Initial Deposit",
                            OccerrenceAccount = null,
                            ToAccount = vm.Account.AccountId,
                            TransactionType = TransactionType.Deposit,
                            Notes = $"Initial Deposit for {vm.Account.AccountName} {Constants.Accounts.GetDisplay(vm.Account.AccountType)}"
                        };

                        TransactionDetailBase savedTransaction = transactionsOM.SaveTransaction(InitialDeposit);

                        vm.Account.InitialBalance = savedTransaction.Amount;
                        vm.Account.InitialDepositAccountId = savedTransaction.FromAccount.AccountId;
                        vm.Account.InitialDepositId = savedTransaction.TransactionId;

                        vm.OGAccount = accountsOM.SaveAccount(vm.Account);
                    }
                }
                else
                {
                    if (vm.IsDirty)
                    {
                        vm.OGAccount = accountsOM.SaveAccount(vm.Account);
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
            _OpenTransactionModal(vm.Account.InitialDepositId);
        }

        private void OnModalClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OnModalClose();
        }

        #endregion Form Calls

        #region Private Methods

        private void OnModalClose()
        {
            _OnClose?.Invoke(vm.IsDirty);
        }

        #endregion Private Methods
    }
}
