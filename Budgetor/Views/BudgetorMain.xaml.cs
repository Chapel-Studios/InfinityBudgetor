using Budgetor.Controls;
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

        public AccountsTabVM AccountsTabVM;


        #endregion Properties

        public BudgetorMain()
        {
            InitializeComponent();

            AccountsTabVM = Factory.GetAcountsTabVM();
            AccountsTab.DataContext = AccountsTabVM;
        }

        public void EditBankAccount(int id)
        {
            var editor = new BankAccount_Form(Factory.GetEditBankAccountVM(id));
            editor.Show();
        }

        private void EditBankAccount_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as EditButton;
            EditBankAccount(button.AccountId);
        }
    }
}
