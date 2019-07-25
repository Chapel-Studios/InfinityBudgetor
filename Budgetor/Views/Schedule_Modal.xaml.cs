using Budgetor.Helpers;
using Budgetor.Helpers.Delegates;
using Budgetor.Models;
using Budgetor.Overminds;
using System;
using System.Windows;

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

        public bool IsDirty => !vm.IsEditMode || (Schedule != OGSchedule);

        private Schedule_Base _OGSchedule;
        public Schedule_Base OGSchedule
        {
            get { return _OGSchedule; }
            set
            {
                _OGSchedule = value;

                Schedule = new Schedule_Base()
                {
                    DateTime_Created = value.DateTime_Created,
                    DateTime_Deactivated = value.DateTime_Deactivated,
                    Frequency = value.Frequency,
                    ScheduleId = value.ScheduleId,
                    Occurrence_Final = value.Occurrence_Final,
                    Occurrence_First = value.Occurrence_First,
                    Occurrence_LastConfirmed = value.Occurrence_LastConfirmed,
                    Occurrence_LastPlanned = value.Occurrence_LastPlanned
                };
            }
        }
        public Schedule_Base Schedule { get; set; }

        #endregion Properties

        #region Constructors

        public Schedule_Modal(ManageScheduleVM initialVM, TransactionsOM transactionsOM, string accountName, string accountType, UpdateScheduleDelegate onClose)
        {
            vm = initialVM;
            _OnClose = onClose;
            OGSchedule = vm.Schedule;
            TransactionsOM = transactionsOM;

            InitializeComponent();

            Title = $"Customize schedule for {accountName} ({accountType})";

            VMHandle.DataContext = vm;
            Schedule_Grid.DataContext = Schedule;

            Frequency_ComboBox.BindToList(vm, "Frequencies", "SelectedFrequency");

            Hour_ComboBox.BindToList(vm, "HoursList", "SelectedHour");

            Meridian_ComboBox.BindToList(vm, "MeridianList", "SelectedMeridian", true);
        }

        #endregion Constructors

        #region Form Calls

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDirty)
                {
                    OGSchedule = TransactionsOM.SaveSchedule(Schedule);
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
            _OnClose?.Invoke(IsDirty, Schedule);
        }

        #endregion Private Methods

    }
}
