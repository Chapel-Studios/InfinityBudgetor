using Budgetor.Controls;
using Budgetor.Helpers.Delegates;
using Budgetor.Helpers.Extensions;
using Budgetor.Models.Scheduling;
using Budgetor.Overminds;
using System;
using System.Collections.Generic;
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

            WeekendHandler_ComboBox.BindToList(vm, "WeekendOptions", "SelectedWeekendOption", true);

            // Create date checkboxes
            for (int i = 1; i < 32; i++)
            {
                var dateStr = i.ToString();
                var last = (i % 10);
                double w = 45;
                if (last == 1)
                {
                    dateStr = dateStr + "st";
                }
                else if (last == 2)
                {
                    dateStr = dateStr + "nd";
                }
                else if (last == 3)
                {
                    dateStr = dateStr + "rd";
                }
                else
                {
                    dateStr = dateStr + "th";
                }

                if (i == 31)
                {
                    dateStr += " / Last day of the month";
                    w = double.NaN;
                }

                var cb = new DateSelection_CheckBox()
                {
                    Height = 15,
                    Width = w,
                    Margin = new Thickness(2),
                    Content = dateStr,
                    FontSize = 12,
                    DateValue = i
                };
                cb.Checked += DateCheckBox_Checked;
                cb.Unchecked += DateCheckBox_Checked;

                DateOptions_Panel.Children.Add(cb);
            }
        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            ////this.Close();
            vm.Schedule.Occurrence_LastConfirmed = DateTime.UtcNow;
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

        private void DateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = (DateSelection_CheckBox)sender;
            if (cb.IsChecked.HasValue)
            {
                if (cb.IsChecked.Value)
                {
                    if (!vm.Schedule.SelectedDates.Contains(cb.DateValue))
                    {
                        vm.Schedule.SelectedDates.Add(cb.DateValue);
                    }
                }
                else
                {
                    if (vm.Schedule.SelectedDates.Contains(cb.DateValue))
                    {
                        vm.Schedule.SelectedDates.Remove(cb.DateValue);
                    }
                }
                if (cb.DateValue == 31)
                {
                    vm.Schedule.UsesLastDayOfTheMonth = cb.IsChecked.Value;
                }
            }
            vm.Schedule.SelectedDates.Sort();
        }

        private void IgnoreWeekends_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = (CheckBox)sender;
            if (!cb.IsChecked.HasValue || (cb.IsChecked.HasValue && !cb.IsChecked.Value))
            {
                vm.SelectedWeekendOption = string.Empty;
            }
        }

        #endregion Form Calls

        #region Private Methods

        private void OnModalClose()
        {
            _OnClose?.Invoke(vm.IsDirty, vm.Schedule);
        }

        #endregion Private Methods
    }
}
