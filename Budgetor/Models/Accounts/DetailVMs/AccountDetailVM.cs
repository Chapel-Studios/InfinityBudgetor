﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Budgetor.Constants.Accounts;

namespace Budgetor.Models
{
    public class AccountDetailVM : AccountBasicInfo
    {
        public string Notes { get; set; }
    }
}
