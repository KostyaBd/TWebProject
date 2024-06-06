using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Web.Models;

namespace TWProject.Web.Controllers
{
    public class ListingController : Controller
    {
        private readonly ISession _session;

        public ListingController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Listing
        public ActionResult Index(IEnumerable<CarListing> cars,IEnumerable<CarBookings> bookings)
        {
            var getCars = _session.GetAllCars();
            var getBookingDates = _session.GetBookingDates();
            cars = getCars.Select(c => new CarListing
            {
                Mark = c.Mark,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                BodyType = c.BodyType,
                SeatsNum = c.SeatsNum,
                Color = c.Color,
                Odometer = c.Odometer,
                EngineCapacity = c.EngineCapacity,
                EnginePower = c.EnginePower,
                PricePerDay = c.PricePerDay,
                GearboxType = c.GearboxType,
                FuelType = c.FuelType,
                ImagePath = c.ImagePath,
                IsAvailable = c.IsAvailable
            });

            bookings = getBookingDates.Select(b => new CarBookings
            {
                BookingRecievedDate = b.BookingRecievedDate,
                BookingReturnDate = b.BookingReturnDate
            });

            var viewModel = new CarListingViewModel
            {
	            Cars = cars.ToList(),
	            Bookings = bookings.ToList()
            };
			return View(viewModel);
        }

        
    }
}