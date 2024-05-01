using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UBB_SE_2024_Team_42.Domain.Category;
using UBB_SE_2024_Team_42.Domain.Post.Interfaces;
using UBB_SE_2024_Team_42.Service;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for SearchQuestionPage.xaml
    /// </summary>
    public partial class SearchQuestionPage : Page
    {
        public ObservableCollection<IQuestion> Posts { get; set; }
        public ObservableCollection<ICategory> Categories { get; set; }
        private readonly IService iservice;
        public SearchQuestionPage(IService service)
        {
            InitializeComponent();
            this.iservice = service;
            Posts = new ObservableCollection<IQuestion>(service.SortQuestionsByDateDescending());
            Categories = new ObservableCollection<ICategory>(service.GetAllCategories());
            DataContext = this; // Set DataContext to enable data binding
        }

        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> searchedQuestions = iservice.FindQuestionsByPartialStringInAnyField(SearchBox.Text);
            Posts.Clear();
            foreach (IQuestion question in searchedQuestions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // iservice.getQuestionsOfCategory()
            var selectedCategory = CategorySelector.SelectedItem as Category;
            List<IQuestion> questionsOfCategory = iservice.GetQuestionsOfCategory(selectedCategory);
            // Posts = iservice.getQuestionsOfCategory(selectedCategory) as ObservableCollection<Posts>;
            Posts.Clear();
            foreach (IQuestion question in questionsOfCategory)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void NewestSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions = iservice.SortQuestionsByDateDescending();
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostUpvotesSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions = iservice.GetQuestionsSortedByScoreDescending();
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostAnswers_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions = iservice.SortQuestionsByNumberOfAnswersDescending();
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void HideUnAnsweredCheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void HideUnAnsweredCheckBox_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions;
            if (HideUnAnsweredCheckBox.IsChecked == true)
            {
                questions = iservice.GetQuestionsWithAtLeastOneAnswer();
            }
            else
            {
                questions = iservice.GetCurrentQuestions();
            }
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }
        private void OnQuestion_Click(object sender, RoutedEventArgs e)
        {
            IQuestion myQuestion = (IQuestion)((Button)sender).DataContext;
            SearchFrame.Navigate(new ViewQuestionPage(iservice, myQuestion));
        }

        private void AskQuestion_Click(object sender, RoutedEventArgs e)
        {
            SearchFrame.Navigate(new CreateQuestionPage(iservice));
        }

        private void OpenProfile_Click(object sender, RoutedEventArgs e)
        {
            MiniProfile miniProfile = new (iservice);
            miniProfile.Show();
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsView statistics = new (iservice);
            statistics.Show();
        }
    }
}
