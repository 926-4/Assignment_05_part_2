using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Domain.Tag;
using UBB_SE_2024_Team_42.Service;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for ViewQuestionPage.xaml
    /// </summary>
    public partial class ViewQuestionPage : Page
    {
        private readonly Service.Service service;
        public ObservableCollection<IPost> Comments { get; set; }
        public ObservableCollection<ITag> Tags { get; set; }
        private readonly IQuestion question;
        public ViewQuestionPage(Service.Service service, IQuestion question)
        {
            this.question = question;
            this.service = service;
            InitializeComponent();
            DataContext = this;
            Comments = new ObservableCollection<IPost>(service.GetRepliesOfPost(question.ID));
            Tags = new ObservableCollection<ITag>(service.GetTagsOfQuestion(question.ID));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewQuestionFrame.Navigate(new SearchQuestionPage(service));
            // ViewQuestionFrame.Visibility = Visibility.Collapsed;
        }

        private void ViewQuestionFrame_Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}

