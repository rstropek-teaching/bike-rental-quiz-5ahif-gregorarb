using System;
using System.ComponentModel.DataAnnotations;

namespace Bike_Rental_Service.Models
{
    public class Bike
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Brand { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? PurchaseDate { get; set; }

        [MaxLength(1000)]
        public string Notes { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? LastServiceDate { get; set; }

        [Required]
        [Range(0.00d, double.MaxValue)]
        // to validate if 2 decimal places are entered
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public double FirstHourPrice { get; set; }

        [Required]
        [Range(1.00d, double.MaxValue)]
        // to validate if 2 decimal places are entered
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public double AdditionalHourPrice { get; set; }

        public Category Category { get; set; }
    }
}
