using System;
using System.Windows.Controls;
using System.Windows;

namespace UBB_SE_2024_Team_42.GUI
{
    public partial class CoolTextBox : UserControl
    {
        public CoolTextBox()
        {
            InitializeComponent();
        }

        private void MarkdownTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormatting("*");
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormatting("**");
        }

        public string GetText()
        {
            return MarkdownTextBox.Text;
        }

        public string Text
        {
            get { return MarkdownTextBox.Text; }
            set { MarkdownTextBox.Text = value; }
        }

        // Method to apply formatting to the selected text in the TextBox
        private void ApplyFormatting(string formattingTag)
        {
            int selectionStart = MarkdownTextBox.SelectionStart;
            int selectionLength = MarkdownTextBox.SelectionLength;
            string text = MarkdownTextBox.Text;

            // Apply formatting to selected text
            string newText = text.Insert(selectionStart, formattingTag);
            newText = newText.Insert(selectionStart + selectionLength + formattingTag.Length, formattingTag);



            // Update TextBox text and selection
            MarkdownTextBox.Text = newText;
            MarkdownTextBox.SelectionStart = selectionStart + formattingTag.Length;
            MarkdownTextBox.SelectionLength = selectionLength;
            MarkdownTextBox.Focus();
        }
    }
}
