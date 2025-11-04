using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelapp
{
    internal class Rooms
    {
        [Key]
        public int Id {  get; set; }
        public string Number { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
        public virtual List<Booking>? Bookings { get; set; }
        public DateTime BookingTo { get; set; } = DateTime.MinValue;
    }
}
