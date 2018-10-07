
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Budgetor.Repo
{
    class Repository
    {
        private Budgetor_Context context;

        public Repository()
        {
            context = new Budgetor_Context();

        }


        #region Accounts

        internal List<BankAccountsListView> GetBankAccountsListViews()
        {
            return context.BankAccountsListViews.ToList();
        }

        internal List<IncomeSourcesListView> GetIncomeSourceListViews()
        {
            return context.IncomeSourcesListViews.ToList();

        }

        internal Account SaveAccount(Account account)
        {
            Account record = null;
            if (account.LocalId != 0)
            {
                record = context.Accounts.FirstOrDefault(a => a.LocalId == account.LocalId);
                context.Accounts.Add(record);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record = account;
                record.DateTime_Created = DateTime.UtcNow;
                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

        internal IncomeSource SaveAccount(IncomeSource account)
        {
            IncomeSource record = null;
            record = context.IncomeSources.FirstOrDefault(a => a.Account == account.Account);
            if (record == null)
            {
                record = account;
                context.IncomeSources.Add(record);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record.DefaultToAccount = account.DefaultToAccount;
                record.ExpectedAmount = account.ExpectedAmount;
                record.Schedule = account.Schedule;

                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

        internal DepositAccount SaveAccount(DepositAccount account)
        {
            DepositAccount record = null;
            record = context.DepositAccounts.FirstOrDefault(a => a.Account == account.Account);
            if (record == null)
            {
                record = account;
                context.DepositAccounts.Add(record);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record.InitialDeposit = account.InitialDeposit;
                record.IsActiveCashAccount = account.IsActiveCashAccount;
                record.IsDefault = account.IsDefault;

                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

        internal decimal UpdateIncomeSourceTotal (int account, decimal difference)
        {
            var record = context.IncomeSources.FirstOrDefault(a => a.Account == account);
            if (record == null)
            {
                throw new Exception($"IncomeSource Account {account} not found when attempting to update it's total");
            }
            record.TotalFromSource += difference;

            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();

            return record.TotalFromSource;
        }

        #endregion Accounts

        #region Scheduling

        internal Schedule GetSchedule(int scheduleId)
        {
            return context.Schedules.First(s => s.LocalId == scheduleId);
        }

        #endregion Scheduling
    }
}
