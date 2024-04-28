using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Domain.Posts;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for MiniProfile.xaml
    /// </summary>
    public partial class MiniProfile : Window
    {
        // public ObservableCollection<Badge> Badges { get; set; }
        public ObservableCollection<IQuestion> Questions { get; set; }
        public ObservableCollection<IPost> Answers { get; set; }

        private Service.Service service;
        private WindowManager manager;

        public MiniProfile(WindowManager manager)
        {
            InitializeComponent();
            service = manager.Service;
            this.manager = manager;
            // sorry for what's coming
            // we're getting the profile of the user with id 3
            Answers = new ObservableCollection<IPost>(service.GetCommentsOfUser(3));
            Questions = new ObservableCollection<IQuestion>(service.GetQuestionsOfUser(1));
            //Badges = new ObservableCollection<Badge>(service.getBadgesOfUser(1));
            DataContext = this; // Set DataContext to enable data binding
        }

        private void BadgeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AnswerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}