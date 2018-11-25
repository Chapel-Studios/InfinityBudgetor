

using Budgetor.Repo.Models;

namespace Budgetor.Models
{
    public class AccountDetailVM : AccountBasicInfo
    {
        public string Notes { get; set; }

        public AccountDetailVM(Constants.Accounts.AccountType accountType) : base(accountType)
        {
        }

        public AccountDetailVM(Account account) : base(Constants.Accounts.GetDisplayByTypeName(account.AccountType).Enum)
        {
            AccountName = account.AccountName;
            Notes = account.Notes;
            AccountId = account.LocalId;
            DateTime_Created = account.DateTime_Created;
            DateTime_Deactivated = account.DateTime_Deactivated;
        }
    }
}
