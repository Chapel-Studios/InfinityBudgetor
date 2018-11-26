using Budgetor.Constants;
using Budgetor.Repo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class AccountComboBoxItem : GenericComboBoxItem
    {
        #region Properties

        public string AccountName
        {
            get
            {
                return Display;
            }
            set
            {
                Display = value;
            }
        }

        public int AccountId
        {
            get
            {
                if (!IntValue.HasValue)
                    IntValue = 0;
                return IntValue.Value;
            }
            set
            {
                IntValue = value;
            }
        }

        private AccountType _AccountType;
        public AccountType AccountType
        {
            get => _AccountType;
            set
            {
                _StringValue = Accounts.GetDisplay(value).TypeName;
                _AccountType = value;
            }
        }

        private string _StringValue;
        public override string StringValue
        {
            get => _StringValue;
            set
            {
                _AccountType = Accounts.GetDisplayByTypeName(value).EnumOption;
                _StringValue = value;
            }
        }

        public bool IsDefault { get; set; }

        #endregion Properties

        #region Constructors

        public AccountComboBoxItem(AccountBasicInfo info)
        {
            MapFromAccountBasicInfo(info);
        }

        public AccountComboBoxItem(AccountBasicInfo info, bool isDefault)
        {
            MapFromAccountBasicInfo(info);
            IsDefault = isDefault;
        }

        public AccountComboBoxItem(AccountComboBoxInfo info)
        {
            AccountName = info.AccountName;
            AccountId = info.AccountId;
            AccountType = Accounts.GetDisplayByTypeName(info.AccountType).EnumOption;
            IsDefault = info.IsDefault;
        }

        #endregion Constructors

        #region Private Methods

        private void MapFromAccountBasicInfo(AccountBasicInfo info)
        {
            AccountName = info.AccountName;
            AccountId = info.AccountId;
            AccountType = info.AccountType;
        }

        #endregion Private Methods
    }
}
