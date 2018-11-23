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
using Budgetor.Factories;
using Budgetor.Models;

namespace Budgetor.Views
{
    /// <summary>
    /// Interaction logic for BankAccount_Form.xaml
    /// </summary>
    public partial class BankAccount_Form : Window
    {
        #region Properties

        private BankAccountDetailVM OGAccount
        {
            set
            {
                Account = value;
            }
        }
        public BankAccountDetailVM Account { get; private set;  }

        private bool ShowCreationDateWarning { get; set; }
        public bool IsEditMode { get; set; }
        public bool NeedResponse { get; set; }
        private bool DidUserCancel { get; set; }
        private List<int> FromAccounts { get; set; }
        private TransactionBase InitialDeposit { get; set; }

        #endregion Properties

        #region Constructors

        public BankAccount_Form(EditBankAccountVM vm)
        {
            InitializeComponent();
        }
        
        #endregion Constructors
    }
}
