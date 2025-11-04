using System.Windows;
using System.Linq;
using hotelapp;
using System;

namespace hotelapp
{
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        //Функція авантаження інформації з бази даних
        private void LoadRooms()
        {
            using var db = new DataContext();
            RoomsGrid.ItemsSource = db.Rooms.Where(r => !r.IsAvailable).OrderBy(r => r.Number).ToList();
        }

        //Бронювання обраної кімнати
        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsGrid.SelectedItem is not Rooms sel) { MessageBox.Show("Оберіть кімнату"); return; }
            if (string.IsNullOrWhiteSpace(TxtName.Text) || string.IsNullOrWhiteSpace(TxtPhone.Text)) { MessageBox.Show("Вкажіть ім'я прізвище та телефон"); return; }
            if (!int.TryParse(TxtDays.Text, out var days) || days <= 0) { MessageBox.Show("Невірна кількість днів"); return; }

            using var db = new DataContext();
            var room = db.Rooms.FirstOrDefault(r => r.Id == sel.Id);
            if (room == null) { MessageBox.Show("Кімнату не знайдено"); return; }

            var booking = new Booking
            {
                Name = TxtName.Text.Trim(),
                Phone = TxtPhone.Text.Trim(),
                RoomId = room.Id,
                RoomNumb = room.Number,
                From = System.DateTime.Now.Date,
                To = System.DateTime.Now.Date.AddDays(days),
                TotalPrice = (room.PricePerNight * days)
            };

            room.IsAvailable = true;

            room.BookingTo = DateTime.Now.Date.AddDays(days);

            db.Bookings.Add(booking);
            db.SaveChanges();

            MessageBox.Show($"Кімнату {room.Number} заброньовано на {days} дн. за {(room.PricePerNight * days)}грн");
            LoadRooms();
        }

        private void TxtDays_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CalcTotalPrice();
        }

        private Rooms _selectedRoom;

        private void RoomsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _selectedRoom = RoomsGrid.SelectedItem as Rooms;
            CalcTotalPrice();
        }

        private void CalcTotalPrice()
        {
            if (int.TryParse(TxtDays.Text, out int days) && _selectedRoom != null)
            {
                decimal total = _selectedRoom.PricePerNight * days;

                DoSplati.Text = "До сплати: " + total.ToString() + "грн";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
