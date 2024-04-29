using System.Windows;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private readonly Service.Service service;
        public Statistics(Service.Service service)
        {
            InitializeComponent();
            this.service = service;
            ThisWeek.Text = service.FilterQuestionsByLast7Days().ToString();
            ThisMonth.Text = service.FilterQuestionsAnsweredThisMonth().ToString();
            ThisYear.Text = service.FilterQuestionsAnsweredLastYear().ToString();
        }
    }
}
