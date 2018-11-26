namespace Budgetor.Repo
{
    using Budgetor.Repo.Models;
    using System.Data.Entity;

    public partial class Budgetor_Context : DbContext
    {
        public Budgetor_Context()
            : base("name=Budgetor_Model")
        {
        }
        // Tables
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ScheduleFrequencyType> ScheduleFrequencyTypes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<DepositAccount> DepositAccounts { get; set; }
        public virtual DbSet<IncomeSource> IncomeSources { get; set; }
        // Views
        public virtual DbSet<BankAccountsListView> BankAccountsListViews { get; set; }
        public virtual DbSet<IncomeSourcesListView> IncomeSourcesListViews { get; set; }


    }
}
