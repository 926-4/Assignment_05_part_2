using System.Windows;
using System.Windows.Controls;

namespace UBB_SE_2024_Team_42.GUI
{
    public partial class Comment : UserControl
    {
        public Comment()
        {
            InitializeComponent();
        }

        public static new readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(Comment), new PropertyMetadata(string.Empty));

        public new string Content
        {
            get => (string)GetValue(ContentProperty);
            set { SetValue(ContentProperty, value); }
        }

        public static readonly DependencyProperty DatePostedProperty =
            DependencyProperty.Register("DatePosted", typeof(string), typeof(Comment), new PropertyMetadata(string.Empty));

        public string DatePosted
        {
            get { return (string)GetValue(DatePostedProperty); }
            set { SetValue(DatePostedProperty, value); }
        }

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}