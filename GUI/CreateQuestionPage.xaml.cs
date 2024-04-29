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
        private readonly Service.Service service;
        public ObservableCollection<ICategory> Categories { get; set; }

        public CreateQuestionPage(Service.Service service)
        {
            InitializeComponent();
            this.service = service;
            Categories = new ObservableCollection<ICategory>(service.GetAllCategories());
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

            service.AddQuestion(title, content, category);
            CreateQuestionFrame.Navigate(new SearchQuestionPage(service));
            DataContext = this;
        }

        private void CategoryBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
