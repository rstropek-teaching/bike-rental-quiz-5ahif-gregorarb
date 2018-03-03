using System;
using System.ComponentModel.DataAnnotations;

namespace Bike_Rental_Service.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Bike RentedBike { get; set; }

        [Required]
        public DateTime RentalBegin { get; set; }

        [DateTimeValidation("RentalBegin")]
        public DateTime? RentalEnd { get; set; }

        [Range(1.00d, double.MaxValue)]
        // to validate if 2 decimal places are entered
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public double TotalCosts { get; set; }

        public bool Paid { get; set; }
    }
}
