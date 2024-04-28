using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using UBB_SE_2024_Team_42.Domain.Category;

namespace UBB_SE_2024_Team_42.GUI
{
    /// <summary>
    /// Interaction logic for CreateQuestionPage.xaml
    /// </summary>
    public partial class CreateQuestionPage : Page
    {
        private readonly WindowManager manager;
        public ObservableCollection<ICategory> Categories { get; set; }

        public CreateQuestionPage(WindowManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            Categories = new ObservableCollection<ICategory>(manager.Service.GetAllCategories());
            DataContext = this;
        }

        private void CoolTextBox_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text;
            string content = ContentBox.GetText();
            Category category = (Category)CategoryBox1.SelectedItem;

            manager.Service.AddQuestion(title, content, category);
            CreateQuestionFrame.Navigate(new SearchQuestionPage(manager));
            DataContext = this;
        }

        private void CategoryBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
