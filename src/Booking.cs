using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotelapp
{
    internal class Booking
    {
        [Key]
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomNumb { get; set; }
        public Rooms? Room { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
