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
            //optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = BikeRentalDatabase; Trusted_Connection = True");
            optionsBuilder.UseSqlServer("Server=tcp:bikerentalserver.database.windows.net,1433;Initial Catalog=bikerentaldb;Persist Security Info=False;User ID=admin;Password=ABC123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        }
        public BikeRentalContext()
        {
        }

        public BikeRentalContext(DbContextOptions<BikeRentalContext> opt) : base(opt)
        {
        }
    }
}
