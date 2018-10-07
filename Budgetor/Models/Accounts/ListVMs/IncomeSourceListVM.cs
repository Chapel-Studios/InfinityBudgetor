using Budgetor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    public class IncomeSourceListVM : AccountListBaseVM
    {
        public DateTime? DateTime_NextTransaction { get; set; }

        public string NextPayDate => DispayFormatHelper.GetDisplayDate(DateTime_NextTransaction.Value);

        public decimal ExpectedAmount { get; set; }

        public string ExpectedAmount_Display => DispayFormatHelper.GetDisplayAmountText(ExpectedAmount);

        public string PayCycle { get; set; }
    }
}
