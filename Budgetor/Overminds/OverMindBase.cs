using Budgetor.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Overminds
{
    public class OverMindBase
    {
        private Repository _Repo;
        protected Repository Repo
        {
            get
            {
                if (_Repo == null)
                {
                    _Repo = new Repository();
                }
                return _Repo;
            }
        }

        private AccountsOM _AccountsOM;
        protected AccountsOM AccountsOM
        {
            get
            {
                if (_AccountsOM == null)
                {
                    _AccountsOM = new AccountsOM();
                }
                return _AccountsOM;
            }
        }
    }
}
