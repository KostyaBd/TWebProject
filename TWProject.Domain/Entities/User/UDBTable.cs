using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Enums;

namespace TWProject.Domain.Entities.User
{
	public class UDBTable
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public URoles level { get; set; }
    }
}
