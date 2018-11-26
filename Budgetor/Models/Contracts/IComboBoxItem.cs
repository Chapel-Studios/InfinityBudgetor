using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetor.Models.Contracts
{
    public interface IComboBoxItem
    {
        string Display { get; set; }
        int? IntValue { get; set; }
        string StringValue { get; set; }
    }
}
