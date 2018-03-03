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
        [MaxLength(50)]
        public string Street { get; set; }

        [Range(0,int.MaxValue)]
        public int HouseNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int ZipCode { get; set; }

        [Required]
        [MaxLength(50)]
        public string Town { get; set; }
    }
}
