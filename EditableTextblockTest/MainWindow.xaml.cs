using System.Windows;

namespace EditableTextblockTest
{
    public partial class MainWindow
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MainWindow), new PropertyMetadata(string.Empty));

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Text = "This is a test";
        }

        private void OnChangeClick(object sender, RoutedEventArgs e)
        {
            Text = "Changed";
        }
    }
}
