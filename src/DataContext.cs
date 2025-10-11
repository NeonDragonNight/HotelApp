using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace hotelapp
{
    internal class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = HotelData2.db");
        }
        public DbSet<Rooms> Rooms {  get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
