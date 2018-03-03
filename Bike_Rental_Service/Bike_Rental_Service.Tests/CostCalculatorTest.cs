using Bike_Rental_Service.Controllers;
using Bike_Rental_Service.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bike_Rental_Service.Tests
{
    [TestClass]
    public class CostCalculatorTest
    {
        RentalController rc = new RentalController();

        [TestMethod]
        public void TotalCostTest1()
        {
            Rental rental = new Rental 
            {
                RentedBike = new Bike
                {
                    FirstHourPrice = 3,
                    AdditionalHourPrice = 5
                },
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 10, 30, 0)
            };

            var total = rc.CalculateTotalCost(rental);

            Assert.AreEqual(13, total);
        }

        [TestMethod]
        public void TotalCostTest2()
        {
            Rental rental = new Rental
            {
                RentedBike = new Bike
                {
                    FirstHourPrice = 5,
                    AdditionalHourPrice = 8
                },
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 10, 14, 0)
            };

            var total = rc.CalculateTotalCost(rental);

            Assert.AreEqual(13, total);
        }

        [TestMethod]
        public void TotalCostTest3()
        {
            Rental rental = new Rental
            {
                RentedBike = new Bike
                {
                    FirstHourPrice = 3,
                    AdditionalHourPrice = 5
                },
                RentalBegin = new DateTime(2018, 2, 14, 8, 15, 0),
                RentalEnd = new DateTime(2018, 2, 14, 8, 25, 0)
            };

            var total = rc.CalculateTotalCost(rental);

            Assert.AreEqual(0, total);
        }
    }
}
