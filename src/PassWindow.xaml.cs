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

namespace hotelapp
{
    /// <summary>
    /// Логика взаимодействия для PassWindow.xaml
    /// </summary>
    public partial class PassWindow : Window
    {
        public PassWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Pass.Password == "1234") 
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                Close();
            }
            else MessageBox.Show("Неврний пароль");
        }
    }
}
