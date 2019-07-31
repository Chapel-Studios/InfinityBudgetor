using Budgetor.Models.Scheduling;
using Budgetor.Models.Shared;
using System.Collections.Generic;

namespace Budgetor.Models.Accounts
{
    public class ManageIncSourceVM : BindableBase
    {
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
                        ScheduleId = value.Schedule.ScheduleId,
                        Occurrence_Final = value.Schedule.Occurrence_Final,
                        Occurrence_First = value.Schedule.Occurrence_First,
                        Occurrence_LastConfirmed = value.Schedule.Occurrence_LastConfirmed,
                        Occurrence_LastPlanned = value.Schedule.Occurrence_LastPlanned,
                        HasCustomTransactionTime = value.Schedule.HasCustomTransactionTime,
                        IsAutoConfirm = value.Schedule.IsAutoConfirm,
                        UsesLastDayOfTheMonth = value.Schedule.UsesLastDayOfTheMonth
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

        private IncomeSourceDetailVM _Account;
        public IncomeSourceDetailVM Account
        {
            get => _Account;
            set
            {
                if (value != _Account)
                {
                    RaisePropertyChanged("Account");
                    _Account = value;
                }
            }
        }

        public bool IsEditMode { get; set; }
        public bool IsDirty => !IsEditMode || (Account != OGAccount);

        public List<AccountComboBoxItem> ToAccounts { get; set; }
        public List<FrequencyComboBoxItem> Frequencies { get; set; }

        private int? _SelectedToAccount;
        public int SelectedToAccount
        {
            get
            {
                if (!_SelectedToAccount.HasValue)
                {
                    _SelectedToAccount = Account.DefaultToAccountId;
                }
                return _SelectedToAccount ?? 0;
            }
            set
            {
                _SelectedToAccount = value;
                RaisePropertyChanged("SelectedToAccount");
            }
        }

        private int? _SelectedFrequency;
        public int SelectedFrequency
        {
            get
            {
                if (!_SelectedFrequency.HasValue)
                {
                    _SelectedFrequency = (int?)Account.Schedule?.Frequency ?? 0;
                }
                return _SelectedFrequency.Value;
            }
            set
            {
                _SelectedFrequency = value;
                RaisePropertyChanged("SelectedFrequency");
            }
        }

        public ManageIncSourceVM(IncomeSourceDetailVM incSource)
        {
            OGAccount = incSource;
            ToAccounts = new List<AccountComboBoxItem>();
            Frequencies = new List<FrequencyComboBoxItem>();
        }
    }
}
