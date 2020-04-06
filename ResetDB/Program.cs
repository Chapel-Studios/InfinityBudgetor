using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace ResetDB
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["BudgetorDB"].ConnectionString;

            FileInfo file = new FileInfo(@"C:\Users\revja\Documents\Source\Repos\Budgetor\Budgetor\DBScripts\Tables.sql");

            string script = file.OpenText().ReadToEnd().Replace("GO", "");

            SqlConnection conn = new SqlConnection(sqlConnectionString);
            var cmd = new SqlCommand(script, conn);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }
    }
}
