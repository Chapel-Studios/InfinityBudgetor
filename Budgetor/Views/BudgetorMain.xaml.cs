using Budgetor.Factories;
using Budgetor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BudgetorMain.xaml
    /// </summary>
    public partial class BudgetorMain : Window
    {
        #region Properties

        private AccountFactory _Factory;
        public AccountFactory Factory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new AccountFactory();
                }

                return _Factory;
            }
        }

        public AccountsTabVM AccountsTab;


        #endregion Properties

        public BudgetorMain()
        {
            InitializeComponent();
            AccountsTab = Factory.GetAcountsTabVM();
            
        }
    }
}
