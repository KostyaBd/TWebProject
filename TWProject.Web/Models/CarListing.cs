using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWProject.Domain.Enums;

namespace TWProject.Web.Models
{
	public class CarListing
	{
		public int CarId { get; set; }
		public string Mark { get; set; }
		public string Model { get; set; }
		public int ProductionYear { get; set; }
		public BodyType BodyType { get; set; }
		public int SeatsNum { get; set; }
		public string Color { get; set; }
		public int Odometer { get; set; }
		public decimal EnginePower { get; set; }
		public decimal EngineCapacity { get; set; }
		public decimal PricePerDay { get; set; }
		public GearboxType GearboxType { get; set; }
		public FuelType FuelType { get; set; }
		public string ImagePath { get; set;}
		public bool IsAvailable { get; set; }
	}
}