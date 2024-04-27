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
    /// Interaction logic for AddComment.xaml
    /// </summary>
    public partial class EditPost : Window
    {
        private Post _post;
        private WindowManager _manager;
        public EditPost(WindowManager manager, Post post)
        {
            _manager = manager;
            _post = post;
            InitializeComponent();
        }

        private void Submit_Button_Click(object sender, RoutedEventArgs e)
        {
            string text = Coolest_TextBox_Ever.Text;
            Post newPost = new Post(_post.PostID, _post.UserID, text, _post.PostType, _post.Reactions, _post.DatePosted, _post.DateOfLastEdit);
            _manager.Repository.updatePost(_post, newPost);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
