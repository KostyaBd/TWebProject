using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWProject.Web.Models
{
	public class CarListingViewModel
	{
		public List<CarListing> Cars { get; set; }
		public List<CarBookings> Bookings { get; set; }
	}
}