using Budgetor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class IncomeSourceListVM : AccountListItemBaseVM
    {
        public int IncomeSourceId { get; set; }

        public DateTime? DateTime_NextTransaction { get; set; }

        public string NextPayDate => getNextPayDateDisplay();

        public decimal ExpectedAmount { get; set; }

        public string UsualAmount => DispayFormatHelper.GetDisplayAmountText(ExpectedAmount);

        public string PayCycle { get; set; }

        private string getNextPayDateDisplay()
        {
            var result = DispayFormatHelper.GetDisplayDate(DateTime_NextTransaction);
            if (string.IsNullOrEmpty(result))
            {
                result = "N/A";
            }
            return result;
        }
    }
}
