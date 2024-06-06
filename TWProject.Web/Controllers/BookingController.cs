using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using TWProject.BusinessLogic;
using TWProject.BusinessLogic.DB;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;
using TWProject.Web.Extentions;
using TWProject.Web.Models;

namespace TWProject.Web.Controllers
{
	public class BookingController : Controller
	{
		private readonly ISession _session;

		public BookingController()
		{
			var bl = new BusinessLogic.BusinessLogic();
			_session = bl.GetSessionBL();
		}

		// GET: Booking
		public ActionResult Index()
		{
			if (!IsUserLoggedIn())
			{
				return RedirectToAction("Index", "Login");
			}

			PopulateCarsDropdown();
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index(CarBookings booking)
		{
			PopulateCarsDropdown();
			UBookingData data = new UBookingData
			{
				CarId = booking.CarId,
				BookingReturnDate = booking.BookingReturnDate,
				BookingRecievedDate = booking.BookingRecievedDate,
				Email = booking.Email
			};

			var carBooking = await _session.UserBooking(data);

			switch (carBooking.Status)
			{
				case 1:
					ViewBag.ErrorMessage = "Invalid dates";
					break;
				case 2:
					return Redirect(carBooking.SessionUrl);
				case 3:
					ViewBag.ErrorMessage = $"Sorry, car is already booked between {data.BookingRecievedDate} and {data.BookingReturnDate}";
					break;
				
			}
			return View(booking);
		}
		
		private bool IsUserLoggedIn()
		{
			var loginStatus = System.Web.HttpContext.Current.Session["LoginStatus"] as string;
			return loginStatus == "login";
		}
		
		private void PopulateCarsDropdown()
		{
			var cars = _session.GetAllCars(); 
			ViewBag.Cars = new SelectList(cars, "CarId", "Model");
		}

		public ActionResult OrderConfirmation()
		{
			ViewBag.Message = "Your booking has been confirmed!";
			return View();
		}
	}
}
