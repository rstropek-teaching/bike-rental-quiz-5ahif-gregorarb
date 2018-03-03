using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bike_Rental_Service.Models;
using Bike_Rental_Service.Data;
using Microsoft.EntityFrameworkCore;

namespace Bike_Rental_Service.Controllers
{
    [Route("api/rentals")]
    public class RentalController : Controller
    {
        private BikeRentalContext db = new BikeRentalContext();

        /// <summary>
        /// Starts a new rental for the specified customer with the specified bike
        /// </summary>
        /// <param name="customerId">Customer who wants to start a new rental</param>
        /// <param name="bikeId">Bike that the customer wants to rent</param>
        /// <returns>Status Code</returns>
        [HttpGet]
        [Route("start")]
        public IActionResult StartRental([FromQuery] int customerId, [FromQuery] int bikeId)
        {
            if (!db.Customers.Any(c => c.Id == customerId) || !db.Bikes.Any(b => b.Id == bikeId))
            {
                return StatusCode(404, "Couldn't create this rental. Please check the customerId and the bikeId you entered.");
            }

            // Check if this customer already has an active rental
            if (db.Rentals.Any(r => r.Customer.Id == customerId && r.RentalBegin != null && r.RentalEnd == null)){
                return StatusCode(400, "This customer already has an active rental.");
            }

                Rental rent = new Rental();

            rent.Customer = db.Customers.FirstOrDefault(c => c.Id == customerId);
            if (rent.Customer == null)
            {
                return StatusCode(404, "The customer could not be found.");
            }

            rent.RentedBike = db.Bikes.FirstOrDefault(b => b.Id == bikeId);
            if (rent.RentedBike == null)
            {
                return StatusCode(404, "The bike could not be found.");
            }

            // Assign the values
            rent.RentalEnd = null;
            rent.RentalBegin = DateTime.Now;
            rent.TotalCosts = 0;
            rent.Paid = false;
            rent.Id = 0;

            db.Rentals.Add(rent);
            db.SaveChanges();
            
            return StatusCode(200, rent);
        }

        /// <summary>
        /// Ends an existing rental
        /// </summary>
        /// <param name="rentalId">ID of the rental that should be ended</param>
        /// <returns>Status Code</returns>
        [HttpGet]
        [Route("end/{rentalId}")]
        public IActionResult EndRental(int rentalId)
        {
            Rental rentToEnd = db.Rentals.Include(r => r.RentedBike).Include(r => r.Customer).FirstOrDefault(r => r.Id == rentalId);
            if (rentToEnd == null)
            {
                return StatusCode(404, "The rental with this ID could not be found.");
            }

            // Check if it was already ended
            if(rentToEnd.RentalEnd != null)
            {
                return StatusCode(400, "The rental has already been ended.");
            }

            // Assign the values
            rentToEnd.RentalEnd = DateTime.Now;
            rentToEnd.TotalCosts = CalculateTotalCost(rentToEnd);

            db.SaveChanges();

            return StatusCode(200, rentToEnd);
        }

        /// <summary>
        /// Sets an existing rental as paid
        /// </summary>
        /// <param name="rentalId">ID of the rental that is being paid</param>
        /// <returns>Status Code</returns>
        [HttpGet]
        [Route("pay/{rentalId}")]
        public IActionResult SetPaid(int rentalId)
        {
            var rent = db.Rentals.FirstOrDefault(r => r.Id == rentalId);

            if (rent.TotalCosts > 0)
            {
                rent.Paid = true;
            }
            else
            {
                return StatusCode(400, "The total costs have not been calculated yet.");
            }

            db.SaveChanges();

            return StatusCode(200, "The rental has been paid.");
        }

        /// <summary>
        /// Returns a List of all unpaid rentals (where TotalCosts > 0 and the rental end has been set)
        /// </summary>
        /// <returns>List of all unpaid rentals</returns>
        [HttpGet]
        [Route("unpaid")]
        public IActionResult GetUnpaid()
        {
            return StatusCode(200, db.Rentals.Where(r => !r.Paid && r.RentalEnd != null && r.TotalCosts > 0).
                SelectMany(retObj => 
                    new Object[] {
                        new {
                            customerId = retObj.Customer.Id,
                            firstName = retObj.Customer.FirstName,
                            lastName = retObj.Customer.LastName,
                            rentalId = retObj.Id,
                            rentalBegin = retObj.RentalBegin,
                            rentalEnd = retObj.RentalEnd,
                            totalCosts = retObj.TotalCosts
                        }
                    }
                ));

        }

        /// <summary>
        /// Returns all rentals
        /// </summary>
        /// <returns>All rentals</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(200, db.Rentals.Include("RentedBike").Include("Customer"));
        }

        /// <summary>
        /// Calculates the total cost of a rent
        /// </summary>
        /// <param name="rent">Rent of which the price should be calculated</param>
        /// <returns>The total cost of a rent</returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        public double CalculateTotalCost(Rental rent)
        {
            TimeSpan? timeDifference = (rent.RentalEnd - rent.RentalBegin);
            var totalMinutes = timeDifference.Value.TotalMinutes;
            var startedHours = (int)Math.Ceiling((totalMinutes) / 60);

            return totalMinutes <= 15 ?
                0 :
                rent.RentedBike.FirstHourPrice + (rent.RentedBike.AdditionalHourPrice * (startedHours - 1));
        }
    }
}
