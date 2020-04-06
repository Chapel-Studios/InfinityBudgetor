namespace Budgetor.Repo.Models
{
    using Budgetor.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IncomeSource
    {
        [Key]
        public int LocalId { get; set; }

        public int AccountId { get; set; }

        public decimal ExpectedAmount { get; set; }

        public decimal TotalFromSource { get; set; }

        public int? DefaultToAccountId { get; set; }

        public int? ScheduleId { get; set; }

        #region Constructors

        public IncomeSource()
        {

        }

        public IncomeSource(IncomeSourceDetailVM source)
        {
            LocalId = source.IncomeSourceId;
            AccountId = source.AccountId;
            DefaultToAccountId = source.DefaultToAccountId;
            ExpectedAmount = source.ExpectedAmount;
            ScheduleId = source.Schedule?.ScheduleId;
        }

        #endregion Constructors

    }
}
