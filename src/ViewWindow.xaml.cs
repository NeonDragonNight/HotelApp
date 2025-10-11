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
            //var free = db.Rooms.Where(r => !r.IsAvailable).OrderBy(r => r.Number).ToList();
            //var occ = db.Rooms.Where(r => r.IsAvailable).OrderBy(r => r.Number).ToList();
            
            //var boo = db.Bookings.OrderByDescending(b => b.To).ToList();

            //FreeList.ItemsSource = free.Select(r => $"{r.Number} — {r.Type} — {r.PricePerNight} грн/день");

            //OccupiedList.ItemsSource = boo.Select(b => $"Кімната: {b.Room.Number} Заброньоавна: {b.Name} тел.{b.Phone} з {b.From} по {b.To}");

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
    }
}
