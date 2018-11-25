using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        private ManageBankAccountVM formVM;

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
                    _SelectedFromAccount = GetDefaultFromAccount();
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

        public BankAccount_Form(ManageBankAccountVM vm, AccountsOM accountOverMind)
        {
            accountsOM = accountOverMind;
            formVM = vm;
            IsEditMode = vm.IsEditMode;
            OGAccount = vm.Account; 
            InitializeComponent();
            Title = formVM.IsEditMode ? formVM.Account.AccountName : "Add a new bank account";
            VMHandle.DataContext = formVM;
            BankAccount_Grid.DataContext = Account;
            FromAccount_ComboBox.ItemsSource = vm.FromAccounts;
            FromAccount_ComboBox.DisplayMemberPath = "AccountName";
            FromAccount_ComboBox.SelectedValuePath = "AccountId";
            FromAccount_ComboBox.SelectedValue = SelectedFromAccount;
        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Deactivate_Button_Click(object sender, RoutedEventArgs e)
        {
            accountsOM.DeactivateAccount(formVM.Account.AccountId);
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
                    TransactionType = Constants.Transactions.TransactionType.Deposit
                };
                accountsOM.SaveTransaction(InitialDeposit);
            }
            this.Close();
        }

        private void EditTransaction_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion Form Calls

        #region Private Methods

        private int GetDefaultFromAccount()
        {
            if (Account.InitialDepositAccountId.HasValue)
            {
                return Account.InitialDepositAccountId.Value;
            }
            else
            {
                if (formVM == null)
                    return 1; // This should always be the 'don't track' inc source

                List<int> defaults = new List<int>();
                foreach (var option in formVM.FromAccounts)
                {
                    if (option.IsCategoryDefault)
                        defaults.Add(option.AccountId);
                }

                if (defaults.Count == 0)
                {
                    return 1;
                }
                else if (defaults.Count == 1)
                {
                    return defaults[0];
                }
                else
                {
                    //TODO: maybe revisit this logic later, maybe return certian account type first?
                    return defaults.Min(); //returns the oldest min
                }
            }
        }

        #endregion Private Methods
    }
}
