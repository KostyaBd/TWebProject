using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Enums;

namespace TWProject.Domain.Entities.Car
{
	public class CarDBTable
	{
		[Key]
		public int CarId { get; set; }
		public string Mark { get; set; }
		public string Model { get; set; }
		public int ProductionYear { get; set; }
		public BodyType BodyType { get; set; }
		public int SeatsNum { get; set; }
		public string Color { get; set; }
		public int Odometer { get; set; }
		public int EnginePower { get; set; }
		public int EngineCapacity { get; set; }
		public decimal PricePerDay { get; set; }
		public GearboxType GearboxType { get; set; }
		public FuelType FuelType { get; set; }
	}
}
