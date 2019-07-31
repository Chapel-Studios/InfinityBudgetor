using Budgetor.Helpers.Extensions;
using System;

namespace Budgetor.Models.Accounts
{
    public class IncomeSourceListVM : AccountListItemBaseVM
    {
        public int IncomeSourceId { get; set; }

        public DateTime? DateTime_NextTransaction { get; set; }

        public string NextPayDate => getNextPayDateDisplay();

        public decimal ExpectedAmount { get; set; }

        public string UsualAmount => DispayExtensions.GetDisplayAmountText(ExpectedAmount);

        public string PayCycle { get; set; }

        private string getNextPayDateDisplay()
        {
            var result = DispayExtensions.GetDisplayDate(DateTime_NextTransaction);
            if (string.IsNullOrEmpty(result))
            {
                result = "N/A";
            }
            return result;
        }
    }
}
