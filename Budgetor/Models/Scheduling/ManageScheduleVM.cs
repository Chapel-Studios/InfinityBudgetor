using Budgetor.Helpers;
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
        private Schedule_Base _Schedule;
        public Schedule_Base Schedule
        {
            get => _Schedule;
            set
            {
                if (value != _Schedule)
                {
                    _Schedule = value;
                    RaisePropertyChanged("Schedule");
                }
            }
        }

        public bool IsEditMode { get; set; }

        public bool IsDirty => !IsEditMode || (Schedule != OGSchedule);



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
                if (value != _SelectedFrequency)
                {
                    _SelectedFrequency = value;
                    RaisePropertyChanged("SelectedFrequency");
                }
            }
        }

        public bool HasEndDate
        {
            get
            {
                return Schedule.Occurrence_Final.HasValue;
            }
            set
            {
                if (value)
                {
                    ///todo: make schedule helper that will set this to next occurance by default
                    Schedule.Occurrence_Final = DateTime.UtcNow;
                }
                else
                {
                    Schedule.Occurrence_Final = null;
                }

                RaisePropertyChanged("HasEndDate");

            }
        }
            

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
                if (Schedule.Occurrence_First.Hour > 12)
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




        public ManageScheduleVM(Schedule_Base sched, 
                                List<FrequencyComboBoxItem> acceptedFrequencies,
                                List<IComboBoxItem> hoursList,
                                List<IComboBoxItem> meridianList,
                                List<IComboBoxItem> timeZoneList
        )
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
            else
            {
                IsEditMode = true;
            }

            Frequencies = acceptedFrequencies;
            _SelectedFrequency = (int?)sched?.Frequency;
            OGSchedule = sched;
            HoursList = new List<IComboBoxItem>();
            HoursList.AddRange(hoursList);
            MeridianList = new List<IComboBoxItem>();
            MeridianList.AddRange(meridianList);
            TimeZoneList = new List<IComboBoxItem>();
            TimeZoneList.AddRange(timeZoneList);
        }
    }
}
