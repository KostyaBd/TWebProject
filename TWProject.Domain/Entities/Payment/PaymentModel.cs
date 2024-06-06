using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Car;

namespace TWProject.Domain.Entities.Payment
{
	public class PaymentModel
	{
		public int Amount { get; set; }
		public string Currency { get; set; }
		public string Token { get; set; }
		public int NumberOfDays { get; set; }
		public CarDBTable Car { get; set; }
	}
}
