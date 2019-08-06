using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Constants
{
    class MiscConstants
    {
        public const string APPNAME = "Budgetor";
        public const string DBPATH = "%LocalAppData%\\" + APPNAME + "\\";
        public const string DBFILENAME = APPNAME + ".db";
        public const string DBFILEPATH = DBPATH + DBFILENAME;
        public const string SQLSCRIPTPATH = "DBScripts/";

    }
}
