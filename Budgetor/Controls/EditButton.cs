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
        public int ContextualId
        {
            get { return (int)this.GetValue(ContextualIdProperty); }
            set { this.SetValue(ContextualIdProperty, value); }
        }
        public static readonly DependencyProperty ContextualIdProperty = DependencyProperty.Register("ContextualId", typeof(int), typeof(EditButton), new PropertyMetadata(0));

        public EditButton()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(EditButton), new FrameworkPropertyMetadata(typeof(EditButton)));
        }
    }
}
