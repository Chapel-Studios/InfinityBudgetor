

using Budgetor.Repo.Models;
using System;

namespace Budgetor.Models
{
    public class AccountDetailVM : AccountBasicInfo
    {
        public string Notes { get; set; }

        public DateTime DateTime_Created_Local
        {
            get
            {
                return DateTime_Created.ToLocalTime();
            }
            set
            {
                DateTime_Created = (DateTime)value.ToUniversalTime();
            }
        }

        public AccountDetailVM(Constants.AccountType accountType) : base(accountType)
        {
        }

        public AccountDetailVM(Account account) : base(Constants.Accounts.GetDisplayByTypeName(account.AccountType).EnumOption)
        {
            AccountName = account.AccountName;
            Notes = account.Notes;
            AccountId = account.LocalId;
            DateTime_Created = account.DateTime_Created;
            DateTime_Deactivated = account.DateTime_Deactivated;
        }
    }
}
