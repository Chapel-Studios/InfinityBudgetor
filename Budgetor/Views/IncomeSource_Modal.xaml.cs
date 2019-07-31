using Budgetor.Constants;
using Budgetor.Helpers.Delegates;
using Budgetor.Helpers.Extensions;
using Budgetor.Models.Accounts;
using Budgetor.Models.Scheduling;
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
        private TransactionsOM TransactionsOM;

        #endregion Properties

        #region Constructors

        public IncomeSource_Modal(
            ManageIncSourceVM initialVM,
            AccountsOM accountOverMind,
            TransactionsOM transactionsOM,
            ModalCloseDelegate onClose = null
        )
        {
            vm = initialVM;
            _OnClose = onClose;
            AccountsOM = accountOverMind;
            TransactionsOM = transactionsOM;

            InitializeComponent();

            Title = vm.IsEditMode ? vm.Account.AccountName : "Add a new IncomeSource";

            VMHandle.DataContext = vm;
            IncSource_Grid.DataContext = vm.Account;

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
            
            vm.Account.DateTime_Deactivated = AccountsOM.DeactivateAccount(vm.Account.AccountId);
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vm.IsDirty)
                {
                    vm.OGAccount = AccountsOM.SaveAccount(vm.Account);
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

        private void EditSchedule_Click(object sender, RoutedEventArgs e)
        {
            EditSchedule();
        }

        #endregion Form Calls

        #region Private Methods

        private void OnModalClose()
        {
            _OnClose?.Invoke(vm.IsDirty);
        }

        private void EditSchedule()
        {
            var editor = new Schedule_Modal(TransactionsOM.GetManageScheduleVM(vm.Account.Schedule?.ScheduleId), TransactionsOM, vm.Account.AccountName, Accounts.GetDisplay(vm.Account.AccountType).DisplayText, UpdateFrequency);
            editor.Show();
        }

        private void UpdateFrequency(bool isDirty, Schedule_Base schedule)
        {
            if (isDirty)
            {
                vm.SelectedFrequency = (int)schedule.Frequency;
                vm.Account.Schedule = schedule;
            }
        }

        #endregion Private Methods
    }
}
