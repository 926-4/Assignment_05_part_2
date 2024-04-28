using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using UBB_SE_2024_Team_42.Service;
using UBB_SE_2024_Team_42.Repository;
using UBB_SE_2024_Team_42.Domain.Posts;
using UBB_SE_2024_Team_42.Domain.category;
namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for SearchQuestionPage.xaml
    /// </summary>
    public partial class SearchQuestionPage : Page
    {
        public ObservableCollection<IQuestion> Posts { get; set; }
        public ObservableCollection<ICategory> Categories { get; set; }
        private Service.Service service;
        private WindowManager manager;
        public SearchQuestionPage(WindowManager manager)
        {
            InitializeComponent();
            service = manager.Service;
            Posts = new ObservableCollection<IQuestion>(service.SortQuestionsByDateDescending());
            this.manager = manager;
            Categories = new ObservableCollection<ICategory>(service.GetAllCategories());
            DataContext = this; // Set DataContext to enable data binding
        }

        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> searchedQuestions = service.FindQuestionsByPartialStringInAnyField(SearchBox.Text);
            Posts.Clear();
            foreach (IQuestion question in searchedQuestions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void CategorySelector_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            //service.getQuestionsOfCategory()
            var selectedCategory = this.CategorySelector.SelectedItem as Category;
            List<IQuestion> questionsOfCategory = service.GetQuestionsOfCategory(selectedCategory);
            //Posts = service.getQuestionsOfCategory(selectedCategory) as ObservableCollection<Posts>;
            Posts.Clear();
            foreach (IQuestion question in questionsOfCategory)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void NewestSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions = service.SortQuestionsByDateDescending();
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostUpvotesSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<IQuestion> questions = service.GetQuestionsSortedByScoreDescending();
            Posts.Clear();
            foreach (IQuestion question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostAnswers_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = service.SortQuestionsByNumberOfAnswersDescending();
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
            List<Question> questions;
            if (HideUnAnsweredCheckBox.IsChecked == true)
            { 
               questions = service.GetQuestionsWithAtLeastOneAnswer();
            }
            else
            {
                questions = service.getCurrentQuestions();
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
            SearchFrame.Navigate(new ViewQuestionPage(manager, myQuestion));
        }

        private void askQuestion_Click(object sender, RoutedEventArgs e)
        {
            SearchFrame.Navigate(new CreateQuestionPage(manager));
        }

        private void openProfile_Click(object sender, RoutedEventArgs e)
        {
            MiniProfile miniProfile = new(manager);
            miniProfile.Show();
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new(manager);
            statistics.Show();
        }
    }
}
