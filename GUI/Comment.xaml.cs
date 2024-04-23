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

        // Dependency property for Content
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(Comment), new PropertyMetadata(""));

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Dependency property for datePosted
        public static readonly DependencyProperty DatePostedProperty =
            DependencyProperty.Register("DatePosted", typeof(string), typeof(Comment), new PropertyMetadata(""));

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