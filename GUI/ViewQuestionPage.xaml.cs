using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.Tag;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for ViewQuestionPage.xaml
    /// </summary>
    public partial class ViewQuestionPage : Page
    {
        private readonly WindowManager manager;
        public ObservableCollection<IPost> Comments { get; set; }
        public ObservableCollection<ITag> Tags { get; set; }
        private readonly IQuestion question;
        public ViewQuestionPage(WindowManager manager, IQuestion question)
        {
            this.question = question;
            this.manager = manager;
            InitializeComponent();
            Service.Service service = this.manager.Service;
            // Service service = manager.Service;
            DataContext = this;
            Comments = new ObservableCollection<IPost>(service.GetRepliesOfPost(question.PostID));
            Tags = new ObservableCollection<ITag>(service.GetTagsOfQuestion(question.PostID));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewQuestionFrame.Navigate(new SearchQuestionPage(manager));
            // ViewQuestionFrame.Visibility = Visibility.Collapsed;
        }

        private void ViewQuestionFrame_Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}

