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
using System.Windows.Navigation;
using System.Windows.Shapes;

// Должен быть пустой. Вся логика отдельными классами, все переменные в логике!!


namespace Test_WPF_App
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = textBox1.Text;
            if (text != "")
            {
                MessageBox.Show($"Введённое сообщение: {text}");
            }
            else
            {
                MessageBox.Show("Ошибка: текст не был введён");
            }
        }
    }
}
