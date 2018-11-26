using Budgetor.Constants;
using Budgetor.Helpers.Extensions;
using Budgetor.Models;
using Budgetor.Overminds;
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
    /// Interaction logic for Transaction_Modal.xaml
    /// </summary>
    public partial class Transaction_Modal : Window
    {
        #region Properties

        private TransactionModalVM vm;
        private TransactionsOM transactionsOM;

        public bool IsEditMode { get; set; }
        public bool IsAddMode
        {
            get { return !IsEditMode; }
            set { IsEditMode = !value; }
        }

        private TransactionDetailBase _OGTransaction;
        public TransactionDetailBase OGTransaction
        {
            get => _OGTransaction;
            set
            {
                _OGTransaction = value;
                Transaction = new TransactionDetailBase(value.TransactionType)
                {
                    Amount = value.Amount,
                    DateTime_Created = value.DateTime_Created,
                    DateTime_Occurred = value.DateTime_Occurred,
                    FromAccount = value.FromAccount,
                    IsConfirmed = value.IsConfirmed,
                    IsUserCreated = value.IsUserCreated,
                    OccerrenceAccount = value.OccerrenceAccount,
                    Title = value.Title,
                    ToAccount = value.ToAccount
                };
            }
        }
        public TransactionDetailBase Transaction { get; set; }

        public int SelectedTransactionType
        {
            get => (int)vm?.Transaction?.TransactionType;
            set => vm.Transaction.TransactionType = (TransactionType)value;
        }

        public bool UseTime { get; set; }
        public int SelectedHour { get; set; }
        public int SelectedMinutes { get; set; }
        public string SelectedMinutesString
        {
            get => SelectedMinutes.ToMinuteString();
            set
            {
                var isInt = int.TryParse(value, out int newVal);
                if (isInt)
                    SelectedMinutes = newVal;
                else
                    SelectedMinutes = 0;
            }
        }
        public string SelectedMeridian { get; set; }

        public bool ShowToAccount { get; set; }
        public bool ShowFromAccount { get; set; }
        public string FromAccountLabelCopy { get; set; }
        public string ToAccountLabelCopy { get; set; }

        public bool ShowCategory { get; set; }
        public int SelectedCategory { get; set; }

        #endregion Properties

        public Transaction_Modal(TransactionModalVM initialVM, TransactionsOM om)
        {
            vm = initialVM;
            transactionsOM = om;
            IsEditMode = vm.IsEditMode;
            OGTransaction = (TransactionDetailBase)initialVM.Transaction;

            InitializeComponent();

            SetTime();
            Title = vm.IsEditMode ? vm.Transaction.Title : "Add a new transaction";
            ConfigureModalForTransactionType();
            ShowCategory = false;

            VMHandle.DataContext = vm;
            Transaction_Grid.DataContext = Transaction;
            TransactionType_ComboBox.BindToList(vm.TransactionTypesList, SelectedTransactionType);
            Hour_ComboBox.BindToList(vm.HoursList, SelectedHour);
            Meridian_ComboBox.BindToList(vm.MeridianList, SelectedMeridian);
            Category_ComboBox.BindToList(vm.CategoryList, SelectedCategory);
        }

        #region Form Calls

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            transactionsOM.DeleteTransaction(Transaction.TransactionId);
        }

        private void Revert_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion Form Calls

        #region Private Methods

        private void SetTime()
        {
            DateTime time;
            if (Transaction.DateTime_Occurred == DateTime.MinValue)
            {
                time = DateTime.Now;
            }
            else
            {
                time = Transaction.DateTime_Occurred;
            }

            SelectedMinutes = time.Minute;
            SelectedHour = time.Hour;
            if (SelectedHour > 12)
            {
                SelectedHour -= 12;
                SelectedMeridian = "PM";
            }
            else
            {
                SelectedMeridian = "AM";
            }
        }

        private void ConfigureModalForTransactionType()
        {
            switch (vm.Transaction.TransactionType)
            {
                case TransactionType.Deposit:
                    ShowFromAccount = true;
                    FromAccountLabelCopy = "Deposit from where?";
                    ShowToAccount = true;
                    ToAccountLabelCopy = "Deposit this into which account?";
                    break;
                case TransactionType.Purchase:
                    ShowFromAccount = true;
                    FromAccountLabelCopy = "Which account did you use to make the purchase?";
                    ShowToAccount = false;
                    ToAccountLabelCopy = "";
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion Private Methods
    }
}
