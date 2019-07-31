using Budgetor.Constants;
using Budgetor.Models.Shared;
using Budgetor.Repo.Models;

namespace Budgetor.Models.Accounts
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
                _StringValue = Constants.Accounts.GetDisplay(value).TypeName;
                _AccountType = value;
            }
        }

        private string _StringValue;
        public override string StringValue
        {
            get => _StringValue;
            set
            {
                _AccountType = Constants.Accounts.GetDisplayByTypeName(value).EnumOption;
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
            AccountType = Constants.Accounts.GetDisplayByTypeName(info.AccountType).EnumOption;
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
