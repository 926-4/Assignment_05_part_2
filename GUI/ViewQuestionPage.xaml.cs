using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Service;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for ViewQuestionPage.xaml
    /// </summary>
    
    public partial class ViewQuestionPage : Page
    {
        private WindowManager _manager;
        public ObservableCollection<Post> Comments { get; set; }
        public ObservableCollection<Tag> Tags  { get; set; }
        private Question _question;
        public ViewQuestionPage(WindowManager manager, Question question)
        {
            _question = question;
            _manager = manager;
            InitializeComponent();
            Service.Service service = _manager.Service;
            //Service service = _manager.Service;
            DataContext = this;
            Comments = new ObservableCollection<Post>(service.getRepliesOfPost(question.PostID));
            Tags = new ObservableCollection<Tag>(service.getTagsOfQuestion(question.PostID));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            ViewQuestionFrame.Navigate(new SearchQuestionPage(_manager));
            //ViewQuestionFrame.Visibility = Visibility.Collapsed;
        }

        private void ViewQuestionFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}

