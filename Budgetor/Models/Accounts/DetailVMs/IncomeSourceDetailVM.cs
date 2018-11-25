using Budgetor.Models.Contracts;
using Budgetor.Models.Scheduling;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class IncomeSourceDetailVM : AccountDetailVM//, IAccountDetail
    {
        public int IncomeSourceId { get; set; }

        public decimal ExpectedAmount { get; set; }

        public decimal TotalFromSource { get; set; }
        
        public int? DefaultToAccountId { get; set; }

        public Scheduling.ScheduleVM Schedule { get; set; }

        #region Constructors

        public IncomeSourceDetailVM() : base(Constants.Accounts.AccountType.IncomeSource)
        {

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
            Schedule = new ScheduleVM(accountSchedule);
            TotalFromSource = source.TotalFromSource;

        }

        #endregion Constructors

    }
}
