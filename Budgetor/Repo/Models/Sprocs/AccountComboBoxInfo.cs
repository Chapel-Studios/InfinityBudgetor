﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Repo.Models
{
    public class AccountComboBoxInfo
    {
        public string AccountName { get; set; }

        public int AccountId { get; set; }

        public string AccountType { get; set; }

        public bool IsDefault { get; set; }
    }
}
