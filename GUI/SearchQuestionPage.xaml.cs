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
using UBB_SE_2024_Team_42.Domain;
using UBB_SE_2024_Team_42.Service;
using UBB_SE_2024_Team_42.Repository;
namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for SearchQuestionPage.xaml
    /// </summary>
    public partial class SearchQuestionPage : Page
    {
        public ObservableCollection<Question> Posts { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        private Service.Service service;
        private WindowManager manager;
        public SearchQuestionPage(WindowManager manager)
        {
            InitializeComponent();
            service = manager.Service;
            Posts = new ObservableCollection<Question>(service.sortQuestionsByDateDescending());
            this.manager = manager;
            Categories = new ObservableCollection<Category>(service.getAllCategories());
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
            List<Question> searchedQuestions = service.searchQuestion(this.SearchBox.Text);
            Posts.Clear();
            foreach (Question question in searchedQuestions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void CategorySelector_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            //service.getQuestionsOfCategory()
            var selectedCategory = this.CategorySelector.SelectedItem as Category;
            List<Question> questionsOfCategory = service.getQuestionsOfCategory(selectedCategory);
            //Posts = service.getQuestionsOfCategory(selectedCategory) as ObservableCollection<Posts>;
            Posts.Clear();
            foreach (Question question in questionsOfCategory)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void NewestSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = service.sortQuestionsByDateDescending();
            Posts.Clear();
            foreach (Question question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostUpvotesSortButton_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = service.sortQuestionsByScoreDescending();
            Posts.Clear();
            foreach (Question question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }

        private void MostAnswers_Click(object sender, RoutedEventArgs e)
        {
            List<Question> questions = service.sortQuestionsByNumberOfAnswersDescending();
            Posts.Clear();
            foreach (Question question in questions)
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
            if (this.HideUnAnsweredCheckBox.IsChecked == true)
            { 
               questions = service.getQuestionsWithAtLeastOneAnswer();
            }
            else
            {
                questions = service.getCurrentQuestions();
            }
            Posts.Clear();
            foreach (Question question in questions)
            {
                Posts.Add(question);
            }
            DataContext = this;
        }
        //OnQuestion_Click
        private void OnQuestion_Click(object sender, RoutedEventArgs e)
        {
            Question myQuestion = (Question)((Button)sender).DataContext;
            SearchFrame.Navigate(new ViewQuestionPage(manager, myQuestion));
        }

        private void askQuestion_Click(object sender, RoutedEventArgs e)
        {
            SearchFrame.Navigate(new CreateQuestionPage(manager));
        }

        private void openProfile_Click(object sender, RoutedEventArgs e)
        {
            //SearchFrame.Navigate(new MiniProfile(manager));
            MiniProfile miniProfile = new MiniProfile(manager);
            miniProfile.Show();
        }

        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(manager);
            statistics.Show();
        }
    }
}
