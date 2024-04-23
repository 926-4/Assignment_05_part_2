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
using UBB_SE_2024_Team_42.Domain;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for SettingsPop_Up.xaml
    /// </summary>
    public partial class SettingsPop_Up : Window
    {
        private WindowManager _manager;
        private long _id;
        private bool _isQuestion;
        public SettingsPop_Up(WindowManager manager, long question_id, bool isQuestion)
        {
            _manager = manager;
            _id = question_id;
            _isQuestion = isQuestion;
            InitializeComponent();
        }

        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
                Question question = _manager.Repository.getQuestion(_id);
                EditPost window = new EditPost(_manager, question);
                this.Close();
                window.Show();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Rares o zis sa fac de la zero da deadlineu ii intr-o ora deci...
        }
    }
}
