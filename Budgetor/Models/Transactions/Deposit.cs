using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models
{
    class DepositDetail : TransactionDetailBase
    {
        public DepositDetail() : base(Constants.TransactionType.Deposit)
        {

        }
    }
}
