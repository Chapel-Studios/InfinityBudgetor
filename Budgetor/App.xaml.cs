using Budgetor.Helpers;
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
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (FirstTimeExperienceHelper FirstTimeInitHelper = new FirstTimeExperienceHelper())
            {
                FirstTimeInitHelper.FirstRunInit();
            }
        }
    }
}
