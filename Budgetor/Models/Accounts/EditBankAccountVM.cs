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

        public List<AccountComboBoxItem> FromAccounts { get; set; }

        public ManageBankAccountVM()
        {
            FromAccounts = new List<AccountComboBoxItem>();
        }
    }
}
