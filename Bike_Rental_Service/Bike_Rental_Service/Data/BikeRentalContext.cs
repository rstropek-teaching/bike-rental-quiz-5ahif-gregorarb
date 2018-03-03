using Bike_Rental_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Rental_Service.Data
{
    public class BikeRentalContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = BikeRentalDB; Trusted_Connection = True");
        }
        public BikeRentalContext()
        {
        }

        public BikeRentalContext(DbContextOptions<BikeRentalContext> opt) : base(opt)
        {
        }
    }
}
