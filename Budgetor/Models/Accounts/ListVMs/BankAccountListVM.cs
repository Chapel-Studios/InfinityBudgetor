using Budgetor.Helpers.Extensions;

namespace Budgetor.Models.Accounts
{
    public class BankAccountsListItemVM : AccountListItemBaseVM
    {
        public int DepositAccountId { get; set; }

        public bool IsDefault { get; set; }

        public bool IsActiveCashAccount { get; set; }

        public decimal Balance { get; set; }

        public string DisplayBalance => DispayExtensions.GetDisplayAmountText(this.Balance);

    }
}
