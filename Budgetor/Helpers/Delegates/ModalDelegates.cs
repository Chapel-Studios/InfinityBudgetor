using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Helpers.Delegates
{
    public delegate void ModalCloseDelegate(bool isUpdateRequired);

    public delegate void OpenTransactionModalDelegate(int? id);
}
