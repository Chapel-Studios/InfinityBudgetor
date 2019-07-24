using Budgetor.Helpers;
using Budgetor.Helpers.Delegates;
using Budgetor.Models;
using Budgetor.Overminds;
using System;
using System.Windows;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for IncomeSource_Modal.xaml
    /// </summary>
    public partial class IncomeSource_Modal : Window
    {
        #region Properties

        private ManageIncSourceVM vm;
        private ModalCloseDelegate _OnClose;
        private AccountsOM AccountsOM;

        public bool IsDirty => !vm.IsEditMode || (Account != OGAccount);

        private IncomeSourceDetailVM _OGAccount;
        public IncomeSourceDetailVM OGAccount
        {
            get { return _OGAccount; }
            set
            {
                _OGAccount = value;
                Schedule_Base sched = null;
                if (value.Schedule != null)
                {
                    sched = new Schedule_Base()
                    {
                        DateTime_Created = value.Schedule.DateTime_Created,
                        DateTime_Deactivated = value.Schedule.DateTime_Deactivated,
                        Frequency = value.Schedule.Frequency,
                        LocalId = value.Schedule.LocalId,
                        Occurrence_Final = value.Schedule.Occurrence_Final,
                        Occurrence_First = value.Schedule.Occurrence_First,
                        Occurrence_LastConfirmed = value.Schedule.Occurrence_LastConfirmed,
                        Occurrence_LastPlanned = value.Schedule.Occurrence_LastPlanned
                    };
                }

                Account = new IncomeSourceDetailVM()
                {
                    AccountId = value.AccountId,
                    IncomeSourceId = value.IncomeSourceId,
                    AccountName = value.AccountName,
                    DateTime_Created = value.DateTime_Created,
                    DateTime_Deactivated = value.DateTime_Deactivated,
                    ExpectedAmount = value.ExpectedAmount,
                    Notes = value.Notes,
                    TotalFromSource = value.TotalFromSource,
                    DefaultToAccountId = value.DefaultToAccountId,
                    Schedule = sched
                };
            }
        }
        public IncomeSourceDetailVM Account { get; set; }

        #endregion Properties

        #region Constructors

        public IncomeSource_Modal(
            ManageIncSourceVM initialVM,
            AccountsOM accountOverMind,
            ModalCloseDelegate onClose = null
        )
        {
            vm = initialVM;
            _OnClose = onClose;
            AccountsOM = accountOverMind;
            OGAccount = initialVM.Account;

            InitializeComponent();

            Title = vm.IsEditMode ? vm.Account.AccountName : "Add a new IncomeSource";

            VMHandle.DataContext = vm;
            IncSource_Grid.DataContext = Account;

            Frequency_ComboBox.BindToList(vm, "Frequencies", "SelectedFrequency");
            ToAccount_ComboBox.BindToList(vm, "ToAccounts", "SelectedToAccount");

        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Deactivate_Button_Click(object sender, RoutedEventArgs e)
        {
            AccountsOM.DeactivateAccount(vm.Account.AccountId);
            Account.DateTime_Deactivated = DateTime.UtcNow;
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!vm.IsEditMode)
                {
                    OGAccount = AccountsOM.SaveAccount(Account);
                    
                }
                else
                {
                    if (IsDirty)
                    {
                        OGAccount = AccountsOM.SaveAccount(Account);
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                //todo: add error logging
            }
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
