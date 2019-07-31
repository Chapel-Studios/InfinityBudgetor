using Budgetor.Helpers.Extensions;
using Budgetor.Models.Scheduling;
using Budgetor.Repo.Models;
using System;
using System.Text.RegularExpressions;

namespace Budgetor.Models.Accounts
{
    public class IncomeSourceDetailVM : AccountDetailVM//, IAccountDetail
    {
        public int IncomeSourceId { get; set; }

        private decimal _ExpectedAmount;
        public decimal ExpectedAmount
        {
            get
            {
                return _ExpectedAmount;
            }
            set
            {
                _ExpectedAmount = value;
            }
        }

        public string ExpectedAmount_Display
        {
            get
            {
                return this.ExpectedAmount.GetDisplayAmountText();
            }
            set
            {
                value = Regex.Replace(value, "[^0-9.]", String.Empty);
                var b = decimal.TryParse(value, out decimal d);
                if (b)
                {
                    this.ExpectedAmount = b ? d : 0M;
                }
            }
        }

        public decimal TotalFromSource { get; set; }
        public string TotalFromSource_Displpay => TotalFromSource.GetDisplayAmountText();

        public int? DefaultToAccountId { get; set; }

        public Schedule_Base Schedule { get; set; }

        #region Constructors

        public IncomeSourceDetailVM() : base(Constants.AccountType.IncomeSource)
        {
            Schedule = new Schedule_Base(null);
        }

        public IncomeSourceDetailVM(IncomeSource_DetailView repoModel, Schedule sched = null) : base(Constants.AccountType.IncomeSource)
        {
            IncomeSourceId = repoModel.IncomeSourceId;
            Notes = repoModel.Notes;
            AccountId = repoModel.AccountId;
            AccountName = repoModel.AccountName;
            DateTime_Created = repoModel.DateTime_Created;
            DateTime_Deactivated = repoModel.DateTime_Deactivated;
            ExpectedAmount = repoModel.ExpectedAmount;
            TotalFromSource = repoModel.TotalFromSource;
            DefaultToAccountId = repoModel.DefaultToAccountId;
            Schedule = new Schedule_Base(sched);
        }

        public IncomeSourceDetailVM(IncomeSource source, AccountDetailVM baseAccount, Schedule accountSchedule) : base(baseAccount.AccountType)
        {
            AccountName = baseAccount.AccountName;
            Notes = baseAccount.Notes;
            IncomeSourceId = source.LocalId;
            AccountId = baseAccount.AccountId;
            DateTime_Created = baseAccount.DateTime_Created;
            DateTime_Deactivated = baseAccount.DateTime_Deactivated;
            DefaultToAccountId = source.DefaultToAccountId;
            ExpectedAmount = source.ExpectedAmount;
            Schedule = accountSchedule == null ? null : new Schedule_Base(accountSchedule);
            TotalFromSource = source.TotalFromSource;

        }

        #endregion Constructors

    }
}
