using System;
using System.ComponentModel.DataAnnotations;

namespace Bike_Rental_Service.Models
{
    public class DateTimeValidation : ValidationAttribute
    {
        // for getting the rentalEndDate
        public string EndDatePropertyName { get; set; }

        public DateTimeValidation(string endDatePropertyName)
        {
            EndDatePropertyName = endDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var rentalBeginDate = (DateTime)value;
            var rentalEndDate = (DateTime)validationContext.ObjectType.GetProperty(EndDatePropertyName).GetValue(validationContext.ObjectInstance, null);

            if (rentalEndDate > rentalBeginDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The date is invalid because it is not later than the Rental Begin Date.");
            }
        }
    }
}
