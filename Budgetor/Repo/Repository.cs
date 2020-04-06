using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Budgetor.Repo
{
    public class Repository
    {
        private Budgetor_Context context;

        public Repository()
        {
            context = new Budgetor_Context();
        }

        #region Accounts

            #region Gets

        internal Account GetAccount(int id)
        {
            return context.Accounts.FirstOrDefault(a => a.LocalId == id);
        }

        internal DepositAccount_DetailView GetDepositAccountDetail(int id)
        {
            var accountIdParameter = new SqlParameter("@accountId", id);

            return context.Database
                .SqlQuery<DepositAccount_DetailView>("GetBankAccount @accountId", accountIdParameter)
                .FirstOrDefault();
        }

        internal List<BankAccountsListView> GetBankAccountsListViews()
        {
            return context.BankAccountsListViews.ToList();
        }

        internal List<IncomeSourcesListView> GetIncomeSourceListViews()
        {
            return context.IncomeSourcesListViews.ToList();
        }

        internal List<AccountComboBoxInfo> GetAccountComboBoxInfo(List<string> getTypes)
        {
            var param = new SqlParameter("@typeList", string.Join(",", getTypes));
            return context.Database
                .SqlQuery<AccountComboBoxInfo>("GetAccountComboBoxInfo @typeList", param)
                .ToList();
        }

        #endregion Gets

        #region Saves

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
        
            #endregion Saves

            #region Deactivation

        internal Account DeactivateAccount(int accountId)
        {
            if (accountId == 0)
                throw new Exception("Cannot deactivate account with an ID of 0");

            Account record = context.Accounts.FirstOrDefault(a => a.LocalId == accountId);
            record.DateTime_Deactivated = DateTime.UtcNow;
            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();

            return record;
        }

        internal DepositAccount DeactivateDepositAccount(int accountId)
        {
            try
            {
                DepositAccount record = context.DepositAccounts.FirstOrDefault(a => a.AccountId == accountId);
                record.IsActiveCashAccount = false;
                record.IsDefault = false;

                context.Entry(record).State = EntityState.Modified;
                context.SaveChanges();

                return record;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to deactivate deposit account for {accountId}", ex);
            }
        }

            #endregion Deactivation

        #endregion Accounts

        #region Scheduling

        internal Schedule GetSchedule(int scheduleId)
        {
            return context.Schedules.First(s => s.LocalId == scheduleId);
        }

        #endregion Scheduling

        #region Transactions

            #region Gets

        internal Transaction GetTransactionById(int id)
        {
            return GetTransactionsById(new List<int>() { id }).FirstOrDefault();
        }

        internal IEnumerable<Transaction> GetTransactionsById(List<int> ids)
        {
            return context.Transactions.Where(x => ids.Contains(x.LocalId));
        }

        internal IEnumerable<Transaction> GetTransactions(DateTime beginning, DateTime cutOff)
        {
            return context.Transactions.Where(x => x.DateTime_Occurred < cutOff && x.DateTime_Occurred > beginning);
        }

            #endregion Gets

            #region Saves

        internal Transaction SaveTransaction(Transaction transactionToSave)
        {
            Transaction record = null;
            if (transactionToSave.LocalId == 0)
            {
                record = transactionToSave;
                record.DateTime_Created = DateTime.UtcNow;

                context.Transactions.Add(transactionToSave);
                context.Entry(record).State = EntityState.Added;
            }
            else
            {
                record = context.Transactions.FirstOrDefault(a => a.LocalId == transactionToSave.LocalId);
                record.Amount = transactionToSave.Amount;
                record.DateTime_Occurred = transactionToSave.DateTime_Occurred;
                record.FromAccount = transactionToSave.FromAccount;
                record.IsConfirmed = transactionToSave.IsConfirmed;
                record.Title = transactionToSave.Title;
                record.ToAccount = transactionToSave.ToAccount;
                record.Notes = transactionToSave.Notes;

                context.Entry(record).State = EntityState.Modified;
            }

            context.SaveChanges();

            return record;
        }

            #endregion Saves

            #region Deletes

        internal void DeleteTransaction(int transactionId)
        {
            //TODO: what should this return?
            var record = context.Transactions.Where(x => x.LocalId == transactionId).FirstOrDefault();
            context.Transactions.Remove(record);
            context.Entry(record).State = EntityState.Modified;
            context.SaveChanges();
        }

            #endregion Deletes

        #endregion Transactions

        #region Misc

        internal decimal GetMonthlyStartingBalanceForAccount(int accountId, DateTime month)
        {
            var date = new DateTime(month.Year, month.Month, 1);
            var balance = context.Transactions
                .Where(x => x.ToAccount == accountId
                    && x.DateTime_Occurred < date)
                .Sum(x => x.Amount);
            var deductions = context.Transactions
                .Where(x => x.FromAccount == accountId
                    && x.DateTime_Occurred < date)
                .Sum(x => x.Amount);
            return balance - deductions;
        }

        #endregion Misc
    }
}
