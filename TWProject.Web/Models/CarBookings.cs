using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;

namespace TWProject.Web.Models
{
	public class CarBookings
	{
		public int BookingId { get; set; }
		public DateTime BookingRecievedDate { get; set; }
		public DateTime BookingReturnDate { get; set; }
		public decimal TotalPrice { get; set; }
		public UDBTable User { get; set; }
		public IEnumerable<CarDBTable> Car { get; set; }
		public int CarId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }

	}
}