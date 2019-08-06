using Budgetor.Constants;
using Budgetor.Helpers;
using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
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
            if (!CheckForDBExistence())
            {
                DBConnection = CreateDB();
            }


            using (FirstTimeExperienceHelper FirstTimeInitHelper = new FirstTimeExperienceHelper())
            {
                FirstTimeInitHelper.FirstRunInit();
            }
        }

        private static SQLiteConnection CreateDB()
        {
            var t = "Data Source=|DataDirectory|" + MiscConstants.DBFILENAME + ";Version=3;New=True;Compress=True;";
            //var t2 = "Data Source=" + "test32.db" + ";Version=3;New=True;Compress=True;";
            SQLiteConnection conn = new SQLiteConnection(t);
            try
            {
                //var t3 = Directory.GetFiles(MiscConstants.SQLSCRIPTPATH, "*", SearchOption.AllDirectories);
                foreach (var file in Directory.GetFiles(MiscConstants.SQLSCRIPTPATH, "*", SearchOption.AllDirectories))
                {
                    // make sure is SQL file
                    if (file.EndsWith("sql"))
                    {
                        var filetext = File.ReadAllText(file).Replace("\n"," ").Replace("\r"," ").Replace("  "," ");
                        var queries = filetext.Split(new[] { " GO " }, StringSplitOptions.RemoveEmptyEntries);

                        using (SQLiteCommand cmd = conn.CreateCommand())
                        {
                            conn.Open();
                            foreach (var query in queries)
                            {
                                cmd.CommandText = filetext;
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }
                }


                conn.Close();
            }
            catch(Exception ex)
            {

            }
            return conn;
        }

        private bool CheckForDBExistence()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var location = path + "\\" +  MiscConstants.DBFILENAME;
            var exists = File.Exists(location);
            if (exists)
            {
                // todo: verify tables and system accounts
                try
                {
                    SQLiteConnection conn = new SQLiteConnection("Data Source=" + location + ";");
                    conn.Open();
                    //var tb1 = conn.

                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
