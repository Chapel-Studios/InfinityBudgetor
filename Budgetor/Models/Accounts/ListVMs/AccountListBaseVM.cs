using System;

namespace Budgetor.Models.Accounts
{
    public class AccountListItemBaseVM
    {
        public int AccountId { get; set; }

        public string AccountName { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }
    }
}
