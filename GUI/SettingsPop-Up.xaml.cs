using System.Windows;
using UBB_SE_2024_Team_42.Domain.post.Interfaces;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for SettingsPop_Up.xaml
    /// </summary>
    public partial class SettingsPop_Up : Window
    {
        private readonly WindowManager manager;
        private readonly long id;
        private readonly bool isQuestion;
        public SettingsPop_Up(WindowManager manager, long question_id, bool isQuestion)
        {
            this.manager = manager;
            id = question_id;
            this.isQuestion = isQuestion;
            InitializeComponent();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            IQuestion question = manager.Repository.GetQuestion(id);
            EditPost window = new (manager, question);
            Close();
            window.Show();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Rares o zis sa fac de la zero da deadlineu ii intr-o ora deci...
        }
    }
}
