using Budgetor.Helpers;
using System;
using System.Windows;

namespace Budgetor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            bool hasRan = Budgetor.Properties.Settings.Default.DateTime_FirstRun != DateTime.MinValue;

            if (!hasRan)
            {
                using(FirstTimeInitHelper FirstTimeInitHelper = new FirstTimeInitHelper())
                {
                    FirstTimeInitHelper.CreateDoNotTrackIncomeSource();
                    FirstTimeInitHelper.CreateDefaultCashAccount();
                }
                Budgetor.Properties.Settings.Default.DateTime_FirstRun = DateTime.UtcNow;
                Budgetor.Properties.Settings.Default.Save();
            }

            //todo: turn this test off some day
            Budgetor.Properties.Settings.Default.Reload();
            Budgetor.Properties.Settings.Default.Reset();

        }
    }
}
