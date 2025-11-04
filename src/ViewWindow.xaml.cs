using System.Windows;
using hotelapp;

namespace hotelapp
{
    public partial class ViewWindow : Window
    {
        public ViewWindow()
        {
            InitializeComponent();
            RefreshLists();
        }

        private void RefreshLists()
        {
            using var db = new DataContext();

            BookingsGrid.ItemsSource = db.Bookings.OrderByDescending(b => b.To).ToList();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshLists();
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if (BookingsGrid.SelectedItem is not Booking sel) { MessageBox.Show("Оберіть"); return; }
            using var db = new DataContext();
            var book = db.Bookings.FirstOrDefault(r => r.Id == sel.Id);
            if (book != null)
            {
                db.Bookings.Remove(book);
                db.SaveChanges();
            }
            RefreshLists();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }
    }
}
