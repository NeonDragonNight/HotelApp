using System.Windows;
using System.Linq;
using hotelapp;
using System.Printing.IndexedProperties;
using System.Windows.Controls;

namespace hotelapp
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            LoadRooms();
        }
        
        //Функція авантаження інформації з бази даних
        private void LoadRooms()
        {
            using var db = new DataContext();
            RoomsGrid.ItemsSource = db.Rooms.OrderBy(r => r.Number).ToList();
        }

        //Функція додання інформації до бази даних
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNumber.Text)) { MessageBox.Show("Вкажіть номер"); return; }
            if (!decimal.TryParse(TxtPrice.Text, out var price)) { MessageBox.Show("Невірна ціна"); return; }

            using var db = new DataContext();
            var room = new Rooms { Number = TxtNumber.Text.Trim(), Type = TxtType.Text.Trim(), PricePerNight = price, IsAvailable = IsAvailableCheckBox.IsChecked == true };
            db.Rooms.Add(room);
            db.SaveChanges();
            LoadRooms();
        }

        //Функція редагування інформації обраної кімнати
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsGrid.SelectedItem is not Rooms sel) { MessageBox.Show("Оберіть кімнату"); return; }
            if (!decimal.TryParse(TxtPrice.Text, out var price)) { MessageBox.Show("Невірна ціна"); return; }

            using var db = new DataContext();
            var room = db.Rooms.FirstOrDefault(r => r.Id == sel.Id);
            if (room != null)
            {
                room.Number = TxtNumber.Text.Trim();
                room.Type = TxtType.Text.Trim();
                room.PricePerNight = price;
                room.IsAvailable = IsAvailableCheckBox.IsChecked == true;

                if (IsAvailableCheckBox.IsChecked != true) { room.BookingTo = DateTime.MinValue; }

                db.SaveChanges();
            }
            LoadRooms();
        }

        //Видалення обраної кімнати
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsGrid.SelectedItem is not Rooms sel) { MessageBox.Show("Оберіть кімнату"); return; }
            using var db = new DataContext();
            var room = db.Rooms.FirstOrDefault(r => r.Id == sel.Id);
            if (room != null)
            {
                if (room.IsAvailable) { MessageBox.Show("Не можна видалити зайняту кімнату"); return; }
                db.Rooms.Remove(room);
                db.SaveChanges();
            }
            LoadRooms();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
            Close();
        }
    }
}
