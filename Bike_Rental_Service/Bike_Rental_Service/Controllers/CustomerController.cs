using Bike_Rental_Service.Data;
using Bike_Rental_Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bike_Rental_Service.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private BikeRentalContext db = new BikeRentalContext();

        /// <summary>
        /// Returns all customers, if a last name is specified, all customers with this last name are returned
        /// </summary>
        /// <param name="query">Last name</param>
        /// <returns>All customers</returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string query)
        {
            List<Customer> customers = new List<Customer>();
            if (!string.IsNullOrEmpty(query))
            {
                customers.AddRange(db.Customers.Where(r => r.LastName.ToLower().Equals(query.ToLower())));
            }
            else
            {
                foreach (var item in db.Customers)
                {
                    customers.Add(item);
                }
            }
            return StatusCode(200, customers);
        }

        /// <summary>
        /// Returns all rentals of the customer with the given Id
        /// </summary>
        /// <param name="customerId">All rentals of this customer will be returned</param>
        /// <returns>All rentals of the customer with the given Id</returns>
        [HttpGet]
        [Route("rentals/{customerId}")]
        public IActionResult GetAllRentals(int customerId)
            => StatusCode(200, db.Rentals.Where(e => e.Customer.Id == customerId).ToList());

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="customer">Customer that should be created</param>
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return StatusCode(200, "Customer created.");
            }
            else
            {
                return StatusCode(400, "Customer could not be created.");
            }
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="id">Id of the customer that should be updated</param>
        /// <param name="newCustomer">New Customer</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Customer newCustomer)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(c => id == c.Id);

                if (customer == null)
                {
                    return StatusCode(404, "The customer with the given ID was not found.");
                }

                // Assign the new values to the customer
                customer.Id = 0;
                customer.FirstName = newCustomer.FirstName;
                customer.LastName = newCustomer.LastName;
                customer.Birthdate = newCustomer.Birthdate;
                customer.Gender = newCustomer.Gender;
                customer.Street = newCustomer.Street;
                customer.HouseNumber = newCustomer.HouseNumber;
                customer.ZipCode = newCustomer.ZipCode;
                customer.Town = newCustomer.Town;

                db.Customers.Update(customer);
                db.SaveChanges();

                return StatusCode(200, "The customer " + customer.FirstName + " " + customer.LastName + " was updated successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(404, "The customer with the given ID was not found.");
            }
        }

        /// <summary>
        /// Deletes the customer with the given Id
        /// </summary>
        /// <param name="id">Id of the customer that should be deleted</param>
        /// <returns>Status Code</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.Id == id);
                if (customer == null)
                {
                    return StatusCode(404, "The customer with the given ID was not found.");
                }
                // Also remove all the rentals this customer had
                db.Rentals.RemoveRange(db.Rentals.Where(e => e.Customer.Id == id).ToArray());
                db.Customers.Remove(customer);
                
                db.SaveChanges();

                return StatusCode(200, "The customer " + customer.FirstName + " " + customer.LastName + " was deleted successfully.");
            }
            catch (Exception e)
            {
                return StatusCode(404, "The customer with the given ID was not found.");
            } 
        }
    }
}
