
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWProject.BusinessLogic.DB;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.Booking;
using TWProject.Web.Models;

namespace TWProject.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISession _session;
		public HomeController()
		{
			var bl = new BusinessLogic.BusinessLogic();
			_session = bl.GetSessionBL();
		}

        private CarRentalContext db = new CarRentalContext();
        public ActionResult Index()
        {
	        var getCars = _session.GetAllCars();
	        var cars = getCars.Select(c => new CarListing
	        {
                Mark = c.Mark,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                PricePerDay = c.PricePerDay,
                EnginePower = c.EnginePower,
                ImagePath = c.ImagePath
	        });
            return View(cars);
        }

      
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       /* public ActionResult Listing()
        {
            ViewBag.Message = "All available cars:";
        }*/

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

       /* public ActionResult Payment(int id, decimal price, string mark, string model)
        {
            ViewBag.Price = price;
            ViewBag.Mark = mark;
            ViewBag.Model = model;


            return View();
        }


        [HttpPost]
        public ActionResult ProcessPayment(string cardNumber, string expiryDate, string cvv, int days)
        {
            string mark = ViewBag.Mark;
            string model = ViewBag.Model;
            decimal pricePerDay = ViewBag.Price;

            decimal totalPrice = days * pricePerDay;

            BookingDBTable booking = new BookingDBTable
            {
                BookingRecievedDate = DateTime.Now,
                BookingReturnDate = DateTime.Now.AddDays(days),
                TotalPrice = totalPrice,
                Car = db.Cars.FirstOrDefault(c => c.Mark == mark && c.Model == model)
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            return RedirectToAction("ThankYou");
        }

*/

    }
}
