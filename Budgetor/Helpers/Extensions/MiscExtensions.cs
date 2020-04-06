using System.Windows.Controls;
using System.Windows.Data;

namespace Budgetor.Helpers
{
    public static class MiscExtensions
    {
        public static void BindToList(this ComboBox comboBox, object scopingObject, string listName, string selectedValueName, bool useStringValue = false)
        {
            Binding listBinding = new Binding();
            listBinding.Mode = BindingMode.OneWay;
            listBinding.Source = scopingObject;
            listBinding.Path = new System.Windows.PropertyPath(listName);

            Binding valueBinding = new Binding();
            valueBinding.Mode = BindingMode.TwoWay;
            valueBinding.Source = scopingObject;
            valueBinding.Path = new System.Windows.PropertyPath(selectedValueName);

            comboBox.DisplayMemberPath = "Display";
            comboBox.SelectedValuePath = useStringValue ? "StringValue" : "IntValue";
            comboBox.SetBinding(ComboBox.ItemsSourceProperty, listBinding);
            comboBox.SetBinding(ComboBox.SelectedValueProperty, valueBinding);
        }
    }
}
