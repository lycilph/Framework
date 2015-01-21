using System.Windows;
using System.Windows.Controls;

namespace Framework.UI
{
    public class FillPanel : Panel
    {
        #region Fill attached property

        public static bool GetFill(DependencyObject obj)
        {
            return (bool)obj.GetValue(FillProperty);
        }
        public static void SetFill(DependencyObject obj, bool value)
        {
            obj.SetValue(FillProperty, value);
        }
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.RegisterAttached("Fill", typeof(bool), typeof(FillPanel),
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure));

        #endregion

        protected override Size MeasureOverride(Size available_size)
        {
            foreach (UIElement element in InternalChildren)
                element.Measure(available_size);

            return new Size(available_size.Width, available_size.Height);
        }

        protected override Size ArrangeOverride(Size final_size)
        {
            var accumulated_height = 0.0;
            var fill_children_count = 0;
            foreach (UIElement element in InternalChildren)
            {
                if (GetFill(element))
                    fill_children_count++;
                else
                    accumulated_height += element.DesiredSize.Height;
            }

            var height_per_fill_child = (final_size.Height - accumulated_height) / fill_children_count;

            var current_y = 0.0;
            foreach (UIElement element in InternalChildren)
            {
                if (GetFill(element))
                {
                    element.Arrange(new Rect(0, current_y, element.DesiredSize.Width, height_per_fill_child));
                    current_y += height_per_fill_child;
                }
                else
                {
                    element.Arrange(new Rect(0, current_y, element.DesiredSize.Width, element.DesiredSize.Height));
                    current_y += element.DesiredSize.Height;
                }
            }

            return new Size(final_size.Width, final_size.Height);
        }
    }
}
