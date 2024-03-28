using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;

namespace TWProject.Domain.Entities.Booking
{
	public class BookingDBTable
	{
		[Key]
		public int BookingId { get; set; }
		public DateTime BookingRecievedDate { get; set; }
		public DateTime BookingReturnDate { get; set; }
		public decimal TotalPrice { get; set; }
		public UDBTable User { get; set; }
		public CarDBTable Car { get; set; }
	}
}
