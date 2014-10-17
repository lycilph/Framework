using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Framework.Resources
{
    // http://stackoverflow.com/questions/12125764/change-style-of-last-item-in-listbox?lq=1
    public class IsLastItemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type target_type, object parameter, CultureInfo culture)
        {
            var item = (DependencyObject)values[0];
            var ic = ItemsControl.ItemsControlFromItemContainer(item);

            return ic != null && ic.ItemContainerGenerator.IndexFromContainer(item) == ic.Items.Count - 1;
        }

        public object[] ConvertBack(object value, Type[] target_types, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
