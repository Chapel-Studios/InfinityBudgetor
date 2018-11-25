using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Budgetor.Repo.Models;

namespace Budgetor.Models
{
    public class ManageBankAccountVM : BindableBase
    {
        public BankAccountDetailVM Account { get; set; }

        public bool IsEditMode { get; set; }

        public List<FromAccountComboItem> FromAccounts { get; set; }

        public ManageBankAccountVM()
        {
            FromAccounts = new List<FromAccountComboItem>();
        }
    }

    public class FromAccountComboItem : GenericComboBoxItem
    {

        public FromAccountComboItem(FromAccount from)
        {
            AccountId = from.AccountId;
            AccountName = from.AccountName;
            IsCategoryDefault = from.IsDefault;
        }

        public FromAccountComboItem(int accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }

        public int AccountId { get; set; }

        public String AccountName { get; set; }

        public bool IsCategoryDefault { get; set; }
    }
}
