
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
                record.AccountName = account.AccountName;
                record.Notes = account.Notes;
                context.Entry(record).State = EntityState.Modified;
            }
            else
            {
                record = account;
                record.DateTime_Created = DateTime.UtcNow;
                context.Accounts.Add(record);
                context.Entry(record).State = EntityState.Added;
            }

            context.SaveChanges();

            return record;
        }

        internal IncomeSource SaveAccount(IncomeSource account)
        {
            IncomeSource record = context.IncomeSources.FirstOrDefault(a => a.AccountId == account.AccountId);
            if (record == null)
            {
                record = account;
                context.IncomeSources.Add(record);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record.DefaultToAccountId = account.DefaultToAccountId;
                record.ExpectedAmount = account.ExpectedAmount;
                record.ScheduleId = account.ScheduleId;

                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

        internal DepositAccount SaveAccount(DepositAccount account)
        {
            DepositAccount record = context.DepositAccounts.FirstOrDefault(a => a.AccountId == account.AccountId);
            if (record == null)
            {
                record = account;
                context.DepositAccounts.Add(record);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record.InitialDepositId = account.InitialDepositId;
                record.IsActiveCashAccount = account.IsActiveCashAccount;
                record.IsDefault = account.IsDefault;

                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

        internal decimal UpdateIncomeSourceTotal (int account, decimal difference)
        {
            var record = context.IncomeSources.FirstOrDefault(a => a.AccountId == account);
            if (record == null)
            {
                throw new Exception($"IncomeSource Account {account} not found when attempting to update it's total");
            }
            record.TotalFromSource += difference;

            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();

            return record.TotalFromSource;
        }

        internal void SetAccountToSystem(int account)
        {
            var record = context.Accounts.FirstOrDefault(x => x.LocalId == account);
            if (record == null)
            {
                throw new Exception($"Account {account} not found when attempting to update it to a system account");
            }

            record.IsSystem = true;

            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();
            //todo: Log this!
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
