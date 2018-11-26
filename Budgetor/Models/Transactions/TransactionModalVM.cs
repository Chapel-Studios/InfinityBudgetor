using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetor.Models.Contracts;

namespace Budgetor.Models
{
    public class TransactionModalVM
    {
        public TransactionModalVM(ITransactionDetail transaction,
                                  List<IComboBoxItem> transactionTypesList,
                                  List<IComboBoxItem> hoursList,
                                  List<IComboBoxItem> meridianList,
                                  List<IComboBoxItem> timeZoneList,
                                  List<IComboBoxItem> categoryList,
                                  List<AccountComboBoxItem> accounts,
                                  bool isEditMode
        )
        {
            Transaction = transaction;
            TransactionTypesList = new List<IComboBoxItem>();
            TransactionTypesList.AddRange(transactionTypesList);
            HoursList = new List<IComboBoxItem>();
            HoursList.AddRange(hoursList);
            MeridianList = new List<IComboBoxItem>();
            MeridianList.AddRange(meridianList);
            TimeZoneList = new List<IComboBoxItem>();
            TimeZoneList.AddRange(timeZoneList);
            CategoryList = new List<IComboBoxItem>();
            CategoryList.AddRange(categoryList);
            Accounts = new List<AccountComboBoxItem>();
            Accounts.AddRange(accounts);
            IsEditMode = isEditMode;
        }

        public ITransactionDetail Transaction { get; set; }

        public List<IComboBoxItem> TransactionTypesList { get; set; }

        public List<IComboBoxItem> HoursList { get; set; }

        public List<IComboBoxItem> MeridianList { get; set; }

        public List<IComboBoxItem> TimeZoneList { get; set; }

        public List<IComboBoxItem> CategoryList { get; set; }

        public List<AccountComboBoxItem> Accounts { get; set; }

        public bool IsEditMode { get; set; }
    }
}
