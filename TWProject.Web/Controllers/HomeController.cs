
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.Booking;

namespace TWProject.Web.Controllers
{
	public class HomeController : Controller
	{
        private CarRentalContext db = new CarRentalContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Listing()
        {
            ViewBag.Message = "All available cars:";

            return View(db.Cars.ToList());
        }

        /*public ActionResult Listing()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Message = "All available cars:";
                return RedirectToAction("Listing", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }*/

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Payment(int id, decimal price, string mark, string model)
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



    }
}
