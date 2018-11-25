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
        public Transaction_Modal(TransactionModalVM vm, TransactionsOM transactionsOM)
        {
            InitializeComponent();
        }
    }
}
