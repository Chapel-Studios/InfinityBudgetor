using Budgetor.Helpers;
using Budgetor.Helpers.Delegates;
using Budgetor.Models;
using Budgetor.Overminds;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for Schedule_Modal.xaml
    /// </summary>
    public partial class Schedule_Modal : Window
    {
        #region Properties

        private ManageScheduleVM vm;
        private UpdateScheduleDelegate _OnClose;
        private TransactionsOM TransactionsOM;
        #endregion Properties

        #region Constructors

        public Schedule_Modal(ManageScheduleVM initialVM, TransactionsOM transactionsOM, string accountName, string accountType, UpdateScheduleDelegate onClose)
        {
            vm = initialVM;
            _OnClose = onClose;
            TransactionsOM = transactionsOM;

            InitializeComponent();

            Title = $"Customize schedule for {accountName} ({accountType})";

            VMHandle.DataContext = vm;

            Frequency_ComboBox.BindToList(vm, "Frequencies", "SelectedFrequency");

            Hour_ComboBox.BindToList(vm, "HoursList", "SelectedHour");

            Meridian_ComboBox.BindToList(vm, "MeridianList", "SelectedMeridian", true);
        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //vm.Schedule.Occurrence_LastConfirmed = DateTime.UtcNow;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (vm.IsDirty)
                {
                    vm.Schedule.Frequency = (Constants.FrequencyType)vm.SelectedFrequency;
                    vm.OGSchedule = TransactionsOM.SaveSchedule(vm.Schedule);
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
            _OnClose?.Invoke(vm.IsDirty, vm.Schedule);
        }

        #endregion Private Methods

        private void Frequency_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (TabItem tab in Frequency_Control.Items)
            {
                if (tab.Name == ((FrequencyComboBoxItem)Frequency_ComboBox.SelectedItem).Display)
                {
                    Frequency_Control.SelectedItem = tab;
                    return;
                }
            }
        }
    }
}
