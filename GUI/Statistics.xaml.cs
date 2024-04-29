using System.Windows;
using UBB_SE_2024_Team_42.Domain.post.Interfaces;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private readonly WindowManager manager;
        public Statistics(WindowManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            ThisWeek.Text = FilterQuestionsByLast7Days().ToString();
            ThisMonth.Text = FilterQuestionsAnsweredThisMonth().ToString();
            ThisYear.Text = FilterQuestionsAnsweredLastYear().ToString();
        }

        public int FilterQuestionsByLast7Days()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateSevenDaysAgo = currentDate.AddDays(-7);
            List<IQuestion> questionsWithinLast7Days = manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= dateSevenDaysAgo && question.DatePosted <= currentDate)
                .ToList();

            int numberOfQuestion = 0;
            foreach (IQuestion question in questionsWithinLast7Days)
            {
                numberOfQuestion++;
            }

            return numberOfQuestion;
        }

        public int FilterQuestionsAnsweredThisMonth()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfMonth = new (currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<IQuestion> questionsAnsweredThisMonth = manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfMonth && question.DatePosted <= lastDayOfMonth)
                .ToList();

            int numberOfQuestions = questionsAnsweredThisMonth.Count;
            return numberOfQuestions;
        }

        public int FilterQuestionsAnsweredLastYear()
        {
            DateTime currentDate = DateTime.Now;
            DateTime firstDayOfLastYear = new (currentDate.Year - 1, 1, 1);
            DateTime lastDayOfLastYear = new (currentDate.Year - 1, 12, 31);

            List<IQuestion> questionsAnsweredLastYear = manager.Repository.GetAllQuestions()
                .Where(question => question.DatePosted >= firstDayOfLastYear && question.DatePosted <= lastDayOfLastYear)
                .ToList();

            int numberOfQuestions = questionsAnsweredLastYear.Count;
            return numberOfQuestions;
        }
    }
}
