using Budgetor.Models;
using Budgetor.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Budgetor.Helpers.Extensions
{
    public static class MiscExtensions
    {
        public static void BindToList(this ComboBox comboBox, List<IComboBoxItem> List, int selectedBind)
        {
            comboBox.ItemsSource = List;
            comboBox.DisplayMemberPath = "Display";
            comboBox.SelectedValuePath = "IntValue";
            comboBox.SelectedValue = selectedBind;
        }

        public static void BindToList(this ComboBox comboBox, List<AccountComboBoxItem> List, int selectedBind)
        {
            comboBox.ItemsSource = List;
            comboBox.DisplayMemberPath = "Display";
            comboBox.SelectedValuePath = "IntValue";
            comboBox.SelectedValue = selectedBind;
        }

        public static void BindToList(this ComboBox comboBox, List<IComboBoxItem> List, string selectedBind)
        {
            comboBox.ItemsSource = List;
            comboBox.DisplayMemberPath = "Display";
            comboBox.SelectedValuePath = "StringValue";
            comboBox.SelectedValue = selectedBind;
        }

        public static void BindToList(this ComboBox comboBox, List<AccountComboBoxItem> List, string selectedBind)
        {
            comboBox.ItemsSource = List;
            comboBox.DisplayMemberPath = "Display";
            comboBox.SelectedValuePath = "StringValue";
            comboBox.SelectedValue = selectedBind;
        }

        public static string ToMinuteString(this int i)
        {
            if (i < 10)
                return "0" + i.ToString();
            else return i.ToString();
        }
    }
}
