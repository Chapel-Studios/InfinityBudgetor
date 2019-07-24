using Budgetor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class ManageIncSourceVM : BindableBase
    {
        public IncomeSourceDetailVM Account { get; set; }

        public bool IsEditMode { get; set; }

        public List<AccountComboBoxItem> ToAccounts { get; set; }
        public List<FrequencyComboBoxItem> Frequencies { get; set; }

        private int? _SelectedToAccount;
        public int SelectedToAccount
        {
            get
            {
                if (!_SelectedToAccount.HasValue)
                {
                    _SelectedToAccount = Account.DefaultToAccountId;
                }
                return _SelectedToAccount ?? 0;
            }
            set
            {
                _SelectedToAccount = value;
            }
        }

        private int? _SelectedFrequency;
        public int SelectedFrequency
        {
            get
            {
                if (!_SelectedFrequency.HasValue)
                {
                    _SelectedFrequency = (int?)Account.Schedule?.Frequency ?? 0;
                }
                return _SelectedFrequency.Value;
            }
            set
            {
                _SelectedFrequency = value;
            }
        }

        public ManageIncSourceVM()
        {
            ToAccounts = new List<AccountComboBoxItem>();
        }
    }
}
