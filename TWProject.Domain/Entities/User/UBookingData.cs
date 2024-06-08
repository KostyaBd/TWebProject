using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Car;

namespace TWProject.Domain.Entities.User
{
	public class UBookingData
	{
		public DateTime BookingRecievedDate { get; set; }
		public DateTime BookingReturnDate { get; set; }
		public IEnumerable<CarDBTable> Car { get; set; }
		public UDBTable User { get; set; }
		public string Email { get; set; }
		public int CarId { get; set; }
		public string CookieString { get; set; }
		public int UserId { get; set; }
	}
}
