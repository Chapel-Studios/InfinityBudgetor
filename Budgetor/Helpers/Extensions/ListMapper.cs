using Budgetor.Repo.Models;
using Budgetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Helpers.Extensions
{
    public static class ListMapper
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

        public static int GetDefaultAccount(this List<AccountComboBoxItem> items)
        {
            if (items.Count < 2) // there should always be at least 1 item in the list
                    return 1; // This should always be the 'don't track' inc source

            List<int> defaults = new List<int>();
            foreach (var item in items)
            {
                if (item.IsDefault)
                    defaults.Add(item.AccountId);
            }

            if (defaults.Count == 0)
            {
                return 1;
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
