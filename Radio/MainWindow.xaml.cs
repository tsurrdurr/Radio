using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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
using HtmlAgilityPack;

namespace Radio
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void ButtonGive_Click(object sender, RoutedEventArgs e)
        {
            var url = TextBoxUrl.Text;
            string html = String.Empty;
            var request = WebRequest.Create(url);
            using (var responses = request.GetResponse())
            {
                using (var streams = responses.GetResponseStream())
                using (var readers = new StreamReader(streams))
                {
                    //в переменной html наш сайт
                    html = readers.ReadToEnd();
                    TextBlockResult.Text = html; //выводим значение на форму
                }
            }

         

        }
    }
}
