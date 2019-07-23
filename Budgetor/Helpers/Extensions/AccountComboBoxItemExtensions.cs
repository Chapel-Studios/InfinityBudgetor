using Budgetor.Repo.Models;
using Budgetor.Models;
using System.Collections.Generic;
using System.Linq;

namespace Budgetor.Helpers
{
    public static class AccountComboBoxItemExtensions
    {
        public static List<AccountComboBoxItem> ConvertRepoToView(this List<AccountComboBoxInfo> infos)
        {
            var result = new List<AccountComboBoxItem>();
            foreach (var info in infos)
            {
                result.Add(new AccountComboBoxItem(info));
            }
            return result;
        }

        public static int? GetDefaultAccount(this List<AccountComboBoxItem> items)
        {
            if (items.Count == 0)
                return null;
            if (items.Count == 1) 
                return items.FirstOrDefault().AccountId;

            List<int> defaults = new List<int>();
            foreach (var item in items)
            {
                if (item.IsDefault)
                    defaults.Add(item.AccountId);
            }

            if (defaults.Count == 0)
            {
                return items.Min(x => x.AccountId);
            }
            else if (defaults.Count == 1)
            {
                return defaults[0];
            }
            else
            {
                //TODO: maybe revisit this logic later, maybe return certian account type first?
                return defaults.Min(); //returns the oldest min
            }
        }
    }
}
