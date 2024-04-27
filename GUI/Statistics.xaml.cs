using System;
using System.Collections.Generic;
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
using UBB_SE_2024_Team_42.Domain.Posts;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private WindowManager _manager;
        public Statistics(WindowManager manager)
        {
            InitializeComponent();
            _manager = manager;
            ThisWeek.Text = FilterQuestionsByLast7Days().ToString();
            ThisMonth.Text = FilterQuestionsAnsweredThisMonth().ToString();
            ThisYear.Text = FilterQuestionsAnsweredLastYear().ToString();
        }
        
        public int FilterQuestionsByLast7Days()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateSevenDaysAgo = currentDate.AddDays(-7);
            List<Question> questionsWithinLast7Days = _manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= dateSevenDaysAgo && question.DatePosted <= currentDate)
                .ToList();

            int numberOfQuestion = 0;
            foreach(Question question in questionsWithinLast7Days) 
                numberOfQuestion ++;
            return numberOfQuestion;
        }

        public int FilterQuestionsAnsweredThisMonth()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<Question> questionsAnsweredThisMonth = _manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfMonth && question.DatePosted <= lastDayOfMonth)
                .ToList();

            int numberOfQuestions = questionsAnsweredThisMonth.Count;
            return numberOfQuestions;
        }

        public int FilterQuestionsAnsweredLastYear()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfLastYear = new DateTime(currentDate.Year - 1, 1, 1);
            DateTime lastDayOfLastYear = new DateTime(currentDate.Year - 1, 12, 31);

            List<Question> questionsAnsweredLastYear = _manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfLastYear && question.DatePosted <= lastDayOfLastYear)
                .ToList();

            int numberOfQuestions = questionsAnsweredLastYear.Count;
            return numberOfQuestions;
        }
    }
}
