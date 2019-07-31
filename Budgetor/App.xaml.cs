using Budgetor.Constants;
using Budgetor.Helpers;
using System;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace Budgetor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SQLiteConnection DBConnection;

        public App()
        {
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DBConnection = CreateConnection();
            using (FirstTimeExperienceHelper FirstTimeInitHelper = new FirstTimeExperienceHelper(DBConnection))
            {
                FirstTimeInitHelper.FirstRunInit();
            }
        }

        private static SQLiteConnection CreateConnection()
        {

            SQLiteConnection conn;
            conn = new SQLiteConnection("Data Source=" + MiscConstants.DBFILEPATH + ";Version=3;New=True;Compress=True;");
            try
            {
                conn.Open();
                conn.Close();
            }
            catch(Exception ex)
            {

            }
            return conn;
        }

        private bool CheckForDBExistence()
        {
            var exists = File.Exists(MiscConstants.DBFILEPATH);
            if (exists)
            {
                var db = new SQLiteConnection(MiscConstants.DBFILEPATH);
                var tb1 = db.
            }

        }
    }
}
