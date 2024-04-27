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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UBB_SE_2024_Team_42.Domain.category;
using UBB_SE_2024_Team_42.Service;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for CreateQuestionPage.xaml
    /// </summary>
    public partial class CreateQuestionPage : Page
    {
        WindowManager manager;
        public ObservableCollection<Category> Categories { get; set; }

        public CreateQuestionPage(WindowManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            Categories = new ObservableCollection<Category>(manager.Service.getAllCategories());
            DataContext = this;

        }

        private void CoolTextBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string content=ContentBox.GetText();
            Category category = (Category)CategoryBox1.SelectedItem;

            manager.Service.addQuestion(title, content, category);
            CreateQuestionFrame.Navigate(new SearchQuestionPage(manager));
            DataContext = this;
        }

        private void CategoryBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
