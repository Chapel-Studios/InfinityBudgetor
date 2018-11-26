using Budgetor.Repo.Models;
using Budgetor.Models;
using Budgetor.Constants;
using System;
using System.Collections.Generic;
using Budgetor.Models.Contracts;
using Budgetor.Helpers.Extensions;

namespace Budgetor.Overminds
{
    public class TransactionsOM : OverMindBase
    {
        internal TransactionModalVM GetTransactionModalVM(int? transactionId)
        {
            TransactionDetailBase transaction;
            if (transactionId.HasValue)
            {
                transaction = MapRepoTransactionToTransactionBase(Repo.GetTransactionById(transactionId.Value));
            }
            else
            {
                transaction = new TransactionDetailBase();
            }

            return new TransactionModalVM(transaction,
                                          Transactions.TransactionTypesDropDown,
                                          Time.GetHoursComboBoxItems(),
                                          Time.GetMeridianComboBoxItems(),
                                          Time.GetTimeZonesComboBoxItems(),
                                          new List<IComboBoxItem>() { new GenericComboBoxItem("Not yet Implemented") },
                                          Repo.GetAccountComboBoxInfo(new List<string>(Enum.GetNames(typeof(AccountType)))).ConvertRepoToView(),
                                          transactionId.HasValue
            );
        }

        internal void DeleteTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        internal void SaveTransaction(TransactionSaveModel initialDeposit)
        {
            Repo.SaveTransaction(new Transaction(initialDeposit));
            throw new NotImplementedException(); // what does this return?
        }
    }
}
