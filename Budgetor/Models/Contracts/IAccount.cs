﻿using Budgetor.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Contracts
{
    public interface IAccountDetail
    {
        AccountDetailVM MapToAccountDetailVM();
    }
}
