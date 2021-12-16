using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace Course
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string input = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt1_Click(object sender, RoutedEventArgs e)
        {
            TextBox input = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Input") as TextBox;
            TextBox key = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Key") as TextBox;
            TextBox output = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Output") as TextBox;
            if (string.IsNullOrEmpty(input.Text))
            {
                MessageBox.Show("Введите текст!", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(key.Text))
            {
                MessageBox.Show("Введите ключ!", "Ошибка");
                return;
            }
            foreach (var item in key.Text)
            {
                if (!VigenereCipher.Alph.Contains(char.ToLower(item)))
                {
                    MessageBox.Show("Ключ может содержать только символы русского алфавита!", "Ошибка");
                    return;
                }
            }
            output.Text = VigenereCipher.Crypt(input.Text, key.Text, CryptMode.Encrypt);
        }

        private void Decrypt1_Click(object sender, RoutedEventArgs e)
        {
            TextBox input = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Input") as TextBox;
            TextBox key = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Key") as TextBox;
            TextBox output = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Output") as TextBox;
            if (string.IsNullOrEmpty(input.Text))
            {
                MessageBox.Show("Введите текст!", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(key.Text))
            {
                MessageBox.Show("Введите ключ!", "Ошибка");
                return;
            }
            foreach (var item in key.Text)
            {
                if (!VigenereCipher.Alph.Contains(char.ToLower(item)))
                {
                    MessageBox.Show("Ключ может содержать только символы русского алфавита!", "Ошибка");
                    return;
                }
            }
            output.Text = VigenereCipher.Crypt(input.Text, key.Text, CryptMode.Decrypt);
        }

        private void Encrypt2_Click(object sender, RoutedEventArgs e)
        {
            TextBox key = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Key") as TextBox;
            TextBox output = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Output") as TextBox;
            if (input == null)
            {
                MessageBox.Show("Загрузите файл!", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(key.Text))
            {
                MessageBox.Show("Введите ключ!", "Ошибка");
                return;
            }
            foreach (var item in key.Text)
            {
                if (!VigenereCipher.Alph.Contains(char.ToLower(item)))
                {
                    MessageBox.Show("Ключ может содержать только символы русского алфавита!", "Ошибка");
                    return;
                }
            }
            output.Text = VigenereCipher.Crypt(input, key.Text, CryptMode.Encrypt);
        }

        private void Decrypt2_Click(object sender, RoutedEventArgs e)
        {
            TextBox key = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Key") as TextBox;
            TextBox output = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Output") as TextBox;
            if (input == null)
            {
                MessageBox.Show("Загрузите файл!", "Ошибка");
                return;
            }
            if (string.IsNullOrEmpty(key.Text))
            {
                MessageBox.Show("Введите ключ!", "Ошибка");
                return;
            }
            foreach (var item in key.Text)
            {
                if (!VigenereCipher.Alph.Contains(char.ToLower(item)))
                {
                    MessageBox.Show("Ключ может содержать только символы русского алфавита!", "Ошибка");
                    return;
                }
            }
            output.Text = VigenereCipher.Crypt(input, key.Text, CryptMode.Decrypt);
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            Label label = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("label") as Label;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text file|*.txt|Word document|*.doc;*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileName.EndsWith(".doc") || openFileDialog.FileName.EndsWith(".docx"))
                {
                    Word.Application word = new Word.Application();
                    object fileName = openFileDialog.FileName;
                    Word.Document doc = word.Documents.Open(ref fileName);
                    input = doc.Content.Text;
                    doc.Close();
                    word.Quit();
                    label.Content = openFileDialog.FileName.Split('\\').Last();
                }
                else if (openFileDialog.FileName.EndsWith(".txt"))
                {
                    input = File.ReadAllText(openFileDialog.FileName, Encoding.Default);
                    label.Content = openFileDialog.FileName.Split('\\').Last();
                }
                else
                {
                    MessageBox.Show("Неверное расширение файла. Пожалуйста, используйте только txt, doc или docx");
                    input = null;
                }
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            TextBox output = ((((sender as Button).Parent as Grid).Parent as ContentControl).Parent as Grid).FindName("Output") as TextBox;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file|*.txt|Word document|*.doc;*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                if (saveFileDialog.FileName.EndsWith(".doc") || saveFileDialog.FileName.EndsWith(".docx"))
                {
                    Word.Application word = new Word.Application();
                    Word.Document doc = word.Documents.Add();
                    doc.Content.Text = output.Text;
                    doc.SaveAs2(saveFileDialog.FileName);
                    doc.Close();
                    word.Quit();
                }
                else if (saveFileDialog.FileName.EndsWith(".txt"))
                {
                    File.WriteAllText(saveFileDialog.FileName, output.Text, Encoding.Default);
                }
                else
                {
                    MessageBox.Show("Неверное расширение файла. Пожалуйста, используйте только txt, doc или docx");
                }
            }
        }
    }
}
