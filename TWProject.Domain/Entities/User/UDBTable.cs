using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Car;

namespace TWProject.Domain.Entities.User
{
	public class UDBTable
	{
		[Key]
		public int UserId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string Email { get; set; }
		public DateTime DateReceived { get; set; }
		public DateTime DateReturned { get; set; }
		public int DaysRented { get; set; }
		public decimal TotalPaid { get; set; }
		public bool IsVerified { get; set; }
		public CarDBTable Car { get; set; }
	}
}
