using Bike_Rental_Service.Data;
using Bike_Rental_Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Bike_Rental_Service.Controllers
{
    [Route("api/bikes")]
    public class BikeController : Controller
    {
        private BikeRentalContext db = new BikeRentalContext();

        /// <summary>
        /// Returns all bikes that are currently available
        /// </summary>
        /// <param name="sortCriteria">Specifies after which property should be sorted (FirstHour, AdditionalHour, PurchaseDate)</param>
        /// <returns>All available bikes</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string sortCriteria)
        {
            var availableBikes = db.Bikes.Where(b => !db.Rentals.Any(r => r.RentedBike.Id == b.Id));
            if (sortCriteria == null)
            {
                return StatusCode(200, availableBikes);
            }
            if (sortCriteria.ToLower().Equals("firsthour"))
            {
                return StatusCode(200, availableBikes.OrderBy(p => p.FirstHourPrice));
            }
            else if (sortCriteria.ToLower().Equals("additionalhour"))
            {
                return StatusCode(200, availableBikes.OrderBy(p => p.AdditionalHourPrice));
            }
            else if (sortCriteria.ToLower().Equals("purchasedate"))
            {
                return StatusCode(200, availableBikes.OrderByDescending(p => p.PurchaseDate));
            }
            else
            {
                return StatusCode(400, "Could not get the available bikes.");
            }
        }

        /// <summary>
        /// Creates a new bike
        /// </summary>
        /// <param name="bike">Bike that should be created</param>
        [HttpPost]
        public IActionResult Post([FromBody]Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Bikes.Add(bike);
                db.SaveChanges();
                return StatusCode(200, "Bike created.");
            }
            else
            {
                return StatusCode(400, "Bike could not be created.");
            }
        }

        /// <summary>
        /// Updates a Bike
        /// </summary>
        /// <param name="id">Id of the bike that should be updated</param>
        /// <param name="newBike">New Bike</param>
        /// <returns>Status Code</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Bike newBike)
        {
            try
            {
                Bike bike = db.Bikes.FirstOrDefault(c => id == c.Id);

                if (bike == null)
                {
                    return StatusCode(404, "The bike with the given ID was not found.");
                }

                // Assign the new values to the bike
                bike.Id = 0;
                bike.Brand = newBike.Brand;
                bike.PurchaseDate = newBike.PurchaseDate;
                bike.Notes = newBike.Notes;
                bike.LastServiceDate = newBike.LastServiceDate;
                bike.FirstHourPrice = newBike.FirstHourPrice;
                bike.AdditionalHourPrice = newBike.AdditionalHourPrice;
                bike.Category = newBike.Category;

                db.Bikes.Update(bike);
                db.SaveChanges();

                return StatusCode(200, "The bike was updated successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(404, "The bike with the given ID was not found.");
            }
        }

        /// <summary>
        /// Deletes the bike with the given Id
        /// </summary>
        /// <param name="id">Id of the bike that should be deleted</param>
        /// <returns>Status Code</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Bike bike = db.Bikes.FirstOrDefault(c => c.Id == id);
                if (bike == null)
                {
                    return StatusCode(404, "The bike with the given ID was not found.");
                }
                db.Bikes.Remove(bike);

                db.SaveChanges();

                return StatusCode(200, "The bike was deleted successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(404, "The bike with the given ID was not found.");
            }
        }
    }
}
