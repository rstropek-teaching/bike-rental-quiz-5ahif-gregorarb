using System;
using System.ComponentModel.DataAnnotations;

namespace Bike_Rental_Service.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public Gender Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{MM/dd/yyyy}")]
        public DateTime Birthdate { get; set; }

        [Required]
        [MaxLength(75)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string HouseNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }

        [Required]
        [MaxLength(75)]
        public string Town { get; set; }
    }
}
