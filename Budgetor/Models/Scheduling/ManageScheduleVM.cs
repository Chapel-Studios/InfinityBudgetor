﻿using Budgetor.Helpers;
using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class ManageScheduleVM : BindableBase
    {
        #region Properties

        public Schedule_Base Schedule { get; set; }

        public bool IsEditMode { get; set; }


        public List<FrequencyComboBoxItem> Frequencies { get; set; }
        private int? _SelectedFrequency;
        public int SelectedFrequency
        {
            get
            {
                if (!_SelectedFrequency.HasValue)
                {
                    _SelectedFrequency = (int?)Schedule?.Frequency ?? 0;
                }
                return _SelectedFrequency.Value;
            }
            set
            {
                _SelectedFrequency = value;
            }
        }

        public bool HasEndDate => Schedule.Occurrence_Final.HasValue;

        public string WindowTitle { get; set; }

            #region Time

        public List<IComboBoxItem> HoursList { get; set; }
        public int SelectedHour
        {
            get
            {
                var hour = Schedule.Occurrence_First.Hour;
                return hour > 12 ? hour - 12 : hour;
            }
            set
            {
                var hour = value;
                if (_SelectedMeridian == "PM")
                {
                    hour += 12;
                }
                Schedule.Occurrence_First = new DateTime(Schedule.Occurrence_First.Year,
                                                            Schedule.Occurrence_First.Month,
                                                            Schedule.Occurrence_First.Day,
                                                            hour,
                                                            Schedule.Occurrence_First.Minute,
                                                            Schedule.Occurrence_First.Second);
                RaisePropertyChanged("SelectedHour");
            }
        }

        public int SelectedMinutes { get; set; }
        public string SelectedMinutesString
        {
            get
            {
                return Schedule.Occurrence_First.Minute.ToMinuteString();
            }
            set
            {
                int.TryParse(value, out int newVal);

                Schedule.Occurrence_First = new DateTime(Schedule.Occurrence_First.Year,
                                                            Schedule.Occurrence_First.Month,
                                                            Schedule.Occurrence_First.Day,
                                                            Schedule.Occurrence_First.Hour,
                                                            newVal,
                                                            Schedule.Occurrence_First.Second);
                RaisePropertyChanged("SelectedMinutesString");
            }
        }

        public List<IComboBoxItem> MeridianList { get; set; }
        private string _SelectedMeridian;
        public string SelectedMeridian
        {
            get
            {
                if (SelectedHour > 12)
                {
                    _SelectedMeridian = "PM";
                }
                else
                {
                    _SelectedMeridian = "AM";
                }
                return _SelectedMeridian;
            }
            set
            {
                if (value != _SelectedMeridian)
                {
                    var hour = Schedule.Occurrence_First.Hour;
                    if (value == "PM" && hour < 12)
                    {
                        hour += 12;
                    }
                    else if (value == "AM" && hour >= 12)
                    {
                        hour -= 12;
                    }
                    Schedule.Occurrence_First = new DateTime(Schedule.Occurrence_First.Year,
                                                                Schedule.Occurrence_First.Month,
                                                                Schedule.Occurrence_First.Day,
                                                                hour,
                                                                Schedule.Occurrence_First.Minute,
                                                                Schedule.Occurrence_First.Second);

                    _SelectedMeridian = value;
                    RaisePropertyChanged("SelectedMeridian");
                }
            }
        }

        // TODO: Are we going to implement this?
        public List<IComboBoxItem> TimeZoneList { get; set; }
        private int _SelectedTimeZone;
        public int SelectedTimeZone
        {
            get
            {
                return _SelectedTimeZone;
            }
            set
            {
                if (value != _SelectedTimeZone)
                {
                    _SelectedTimeZone = value;
                    RaisePropertyChanged("SelectedTimeZone");
                }
            }
        }

            #endregion Time

        #endregion Properties




        public ManageScheduleVM(Schedule_Base sched, List<FrequencyComboBoxItem> acceptedFrequencies)
        {
            if (sched == null)
            {
                sched = new Schedule_Base()
                {
                    Frequency = Constants.FrequencyType.Infrequent,
                    Occurrence_First = DateTime.UtcNow,
                    HasCustomTransactionTime = false,
                    IsAutoConfirm = false
                };
            }

            Frequencies = acceptedFrequencies;
            _SelectedFrequency = (int?)sched?.Frequency;
            Schedule = sched;
        }
    }
}
