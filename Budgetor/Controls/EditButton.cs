using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Budgetor.Controls
{
    public partial class EditButton : Button
    {
        public int AccountId
        {
            get { return (int)this.GetValue(AccountIdProperty); }
            set { this.SetValue(AccountIdProperty, value); }
        }
        public static readonly DependencyProperty AccountIdProperty = DependencyProperty.Register("AccountId", typeof(int), typeof(EditButton), new PropertyMetadata(0));

        public EditButton()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(EditButton), new FrameworkPropertyMetadata(typeof(EditButton)));
        }
    }
}
