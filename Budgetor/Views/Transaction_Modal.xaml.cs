using Budgetor.Helpers;
using Budgetor.Models;
using Budgetor.Overminds;
using System.Windows;
using System.Windows.Controls;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for Transaction_Modal.xaml
    /// </summary>
    public partial class Transaction_Modal : Window
    {
        #region Properties

        private TransactionModalVM vm;
        private TransactionsOM transactionsOM;

        #endregion Properties

        #region Constructors

        public Transaction_Modal(TransactionModalVM initialVM, TransactionsOM om)
        {
            vm = initialVM;
            transactionsOM = om;

            InitializeComponent();

            VMHandle.DataContext = vm;

            TransactionType_ComboBox.BindToList(vm, "TransactionTypesList", "SelectedTransactionType");

            Hour_ComboBox.BindToList(vm, "HoursList", "SelectedHour");

            Meridian_ComboBox.BindToList(vm, "MeridianList", "SelectedMeridian", true);

            FromAccount_ComboBox.BindToList(vm, "FromAccounts", "SelectedFromAccount");

            ToAccount_ComboBox.BindToList(vm, "ToAccounts", "SelectedToAccount");

            Category_ComboBox.BindToList(vm, "CategoryList", "SelectedCategory");
        }

        #endregion Constructors

        #region Form Calls

        private void TransactionType_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ConfigureVMAsPerTransactionType();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: 'Are you Sure?' Dialog
            transactionsOM.DeleteTransaction(vm.Transaction.TransactionId);
            this.Close();
        }

        private void Revert_Button_Click(object sender, RoutedEventArgs e)
        {
            vm.ResetVM();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            transactionsOM.SaveTransaction(vm.ExportSaveModel());
            this.Close();
        }

        #endregion Form Calls
    }
}
