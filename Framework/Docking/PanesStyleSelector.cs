using System.Windows;
using System.Windows.Controls;

namespace Framework.Docking
{
    public class PanesStyleSelector : StyleSelector
    {
        public Style ToolStyle { get; set; }

        public Style ContentStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ITool)
                return ToolStyle;

            if (item is IContent)
                return ContentStyle;

            return base.SelectStyle(item, container);
        }
    }
}
