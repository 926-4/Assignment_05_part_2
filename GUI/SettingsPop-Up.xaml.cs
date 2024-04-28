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
