using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Budgetor.Controls
{
    public partial class EditButton : Button
    {
        public int AccountId { get; set; }

        public EditButton() : base()
        {
            //base.BeginInit();
        }
    }
}
