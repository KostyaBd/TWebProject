using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;

namespace TWProject.BusinessLogic.DB
{
	public class CarRentalContext : DbContext
	{
		public CarRentalContext() :base("name=CarRental")
		{
            Database.SetInitializer(new CreateDatabaseIfNotExists<CarRentalContext>());
        }

		public virtual DbSet<UDBTable> User { get; set; }
		public virtual DbSet<CarDBTable> Cars { get; set; }
		public virtual DbSet<BookingDBTable> Bookings { get; set; }
	}
}
