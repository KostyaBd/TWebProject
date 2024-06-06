using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Net.Configuration;
using System.Threading.Tasks;
using AutoMapper;
using TWProject.Domain.Enums;
using Helpers;
using Stripe;
using Stripe.Checkout;
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.Payment;
using TWProject.Helpers;
using Session = TWProject.Domain.Entities.User.Session;

namespace TWProject.BusinessLogic.Core
{
    public class UserApi
    {
        internal ULoginResp UserLoginLogic(ULoginData data)
        {
			UDBTable result;
			var validate = new EmailAddressAttribute();
			if (validate.IsValid(data.Credential))
			{
				var pass = LoginHelper.HashGen(data.Password);
				using (var db = new CarRentalContext())
				{
					result = db.User.FirstOrDefault(u => u.Email == data.Credential && u.Password == pass);
				}

				if (result == null)
				{
					return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
				}

				if (result.level == URoles.Admin)
				{

					return new ULoginResp { Status = true, IsAdmin = true };
				}

				return new ULoginResp { Status = true, IsAdmin = false };
			}
			else
			{
				var pass = LoginHelper.HashGen(data.Password);
				using (var db = new CarRentalContext())
				{
					result = db.User.FirstOrDefault(u => u.Name == data.Credential && u.Password == pass);
				}

				if (result == null)
				{
					return new ULoginResp { Status = false, StatusMsg = "The Username or Password is Incorrect" };
				}

				if (result.level == URoles.Admin)
				{

					return new ULoginResp { Status = true, IsAdmin = true };
				}
				else
				{
				return new ULoginResp { Status = true, IsAdmin = false };
				}
			}
		}

        internal HttpCookie Cookie(string loginCredential)
        {
            var apiCookie = new HttpCookie("X-KEY")
            {
                Value = CookieGenerator.Create(loginCredential)
            };

            using (var db = new SessionContext())
            {
                Database.SetInitializer<CarRentalContext>(new CreateDatabaseIfNotExists<CarRentalContext>());

                var session = db.Sessions.FirstOrDefault(s => s.Username == loginCredential);

                if (session != null)
                {
                    session.CookieString = apiCookie.Value;
                    session.ExpireTime = ValidateDateTime(DateTime.Now.AddMinutes(60));
                    session.Username = loginCredential;
                    db.Entry(session).State = EntityState.Modified;
                }
                else
                {
                    db.Sessions.Add(new Session
                    {
                        Username = loginCredential,
                        CookieString = apiCookie.Value,
                        ExpireTime = ValidateDateTime(DateTime.Now.AddMinutes(60))
                    });
                }

                db.SaveChanges();
            }

            return apiCookie;
        }

        internal UserMini UserCookie(string cookie)
        {
            Session session;
            UDBTable currentUser;

            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }

            if (session == null) return null;

            using (var db = new CarRentalContext())
            {
                var validate = new EmailAddressAttribute();
                if (validate.IsValid(session.Username))
                {
                    currentUser = db.User.FirstOrDefault(u => u.Email == session.Username);
                }
                else
                {
                    currentUser = db.User.FirstOrDefault(u => u.Name == session.Username);
                }
            }

            if (currentUser == null) return null;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UDBTable, UserMini>());
            var mapper = config.CreateMapper();
            var userMini = mapper.Map<UserMini>(currentUser);
            return userMini;
        }

        internal ULoginResp UserRegistrationLogic(URegisterData data)
        {
            using (var db = new CarRentalContext())
            {
                var user = db.User.FirstOrDefault(u => u.Email == data.Email || u.Name == data.Username);
                if (user != null)
                {
                    return new ULoginResp { Status = false, StatusMsg = "User already exists" };
                }

                user = new UDBTable
                {
                    Name = data.Username,
                    Password = LoginHelper.HashGen(data.Password),
                    Email = data.Email,
                    level = URoles.User, 
                    RegistrationDate = DateTime.Now,
                };

                db.User.Add(user);
                db.SaveChanges();
            }

            return new ULoginResp { Status = true, StatusMsg = "User registered successfully." };
        }

        private DateTime ValidateDateTime(DateTime dateTime)
        {
            DateTime minSqlDateTime = new DateTime(1753, 1, 1);
            DateTime maxSqlDateTime = new DateTime(9999, 12, 31);

            if (dateTime < minSqlDateTime)
            {
                return minSqlDateTime;
            }
            else if (dateTime > maxSqlDateTime)
            {
                return maxSqlDateTime;
            }
            else
            {
                return dateTime;
            }
        }

		internal IEnumerable<CarDBTable> GetAllCarsAction()
		{
			using (var context = new CarRentalContext())
			{
				var cars = context.Cars.ToList();
				var currentDate = DateTime.Now;

				var bookings = context.Bookings
					.Where(b => b.BookingRecievedDate <= currentDate && b.BookingReturnDate >= currentDate)
					.ToList();

				var mappedCars = cars.Select(car => new CarDBTable()
				{
					CarId = car.CarId,
					Mark = car.Mark,
					Model = car.Model,
					ProductionYear = car.ProductionYear,
					BodyType = car.BodyType,
					SeatsNum = car.SeatsNum,
					Color = car.Color,
					Odometer = car.Odometer,
					EngineCapacity = car.EngineCapacity,
					EnginePower = car.EnginePower,
					PricePerDay = car.PricePerDay,
					GearboxType = car.GearboxType,
					FuelType = car.FuelType,
					ImagePath = car.ImagePath,
					IsAvailable = !bookings.Any(b => b.Car.CarId == car.CarId)
				});

				return mappedCars;
			}
		}


		internal IEnumerable<BookingDBTable> GetBookingDatesAction()
        {
	        using (var context = new CarRentalContext())
	        {
                var bookingDates = context.Bookings.ToList();
                var mappedDates = bookingDates.Select(dates => new BookingDBTable
                {
                    BookingReturnDate = dates.BookingReturnDate,
                    BookingRecievedDate = dates.BookingRecievedDate
                });
                return mappedDates;
	        }
        }

		public async Task<Stripe.Checkout.Session> PaymentProcessAction(PaymentModel model)
		{
			StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["Stripe:SecretKey"];

			var options = new SessionCreateOptions
			{
				PaymentMethodTypes = new List<string> { "card" },
				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = model.Amount,
							Currency = model.Currency,
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
                                Name = $"Booking {model.Car.Model} {model.Car.Mark} for {model.NumberOfDays} day(s)"
							}
						},
						Quantity = 1
					}
				},
				Mode = "payment",
				SuccessUrl = "http://localhost:58516/Booking/OrderConfirmation",
				CancelUrl = "http://localhost:58516/Booking/Login"
			};

			var service = new SessionService();
			Stripe.Checkout.Session session = await service.CreateAsync(options);
			return session;
		}
		internal async Task<UBookingResp> UserBookingAction(UBookingData data)
        {

	        try
	        {
		        using (var context = new CarRentalContext())
		        {
			        var existingBooking = context.Bookings.FirstOrDefault(b => b.Car.CarId == data.CarId &&
			        (b.BookingRecievedDate < data.BookingReturnDate && b.BookingReturnDate > data.BookingRecievedDate));
			        if (existingBooking == null)
			        { 
				        var user = context.User.FirstOrDefault(u => u.Email == data.Email);
				        var car = context.Cars.FirstOrDefault(c => c.CarId == data.CarId);
				        if (data.BookingRecievedDate > data.BookingReturnDate)
				        {
					        return new UBookingResp { Status = 1}; // invalid dates
				        }
				        
				        var finalPrice = CalculateFinalPrice(data);
				        var paymentModel = new PaymentModel
				        {
                            Amount = (int)(finalPrice*100),
                            Currency = "usd",
                            NumberOfDays = (data.BookingReturnDate - data.BookingRecievedDate).Days,
                            Car = car
				        };
				        var paymentSession = await PaymentProcessAction(paymentModel);
				        if (paymentSession is Stripe.Checkout.Session stripeseSession &&
				            !string.IsNullOrEmpty(stripeseSession.Url))
				        {
					        var newBooking = new BookingDBTable
					        {
                                BookingRecievedDate = data.BookingRecievedDate,
                                BookingReturnDate = data.BookingReturnDate,
                                TotalPrice = finalPrice,
                                Car = car,
                                User = user
					        };
					        context.Bookings.Add(newBooking);
					        await context.SaveChangesAsync();
					        return new UBookingResp { Status = 2, SessionUrl = stripeseSession.Url }; //success
				        }
				        else
				        {
					        return new UBookingResp();

						}
					}
			        else
			        {
				        return new UBookingResp { Status = 3 }; //room not available
			        }
		        }
	        }
	        catch (Exception ex)
	        {
		        Console.WriteLine($"An error occurred while processing the booking: {ex.Message}");
		        Console.WriteLine(ex.StackTrace);
		        return new UBookingResp { Status = 4 };
			}
        }

        internal decimal CalculateFinalPrice(UBookingData data)
        {
	        decimal price = 0;
	        using (var context = new CarRentalContext())
	        {
		        var car = context.Cars.FirstOrDefault(c => c.CarId == data.CarId);
		        if (car != null)
		        {
			        decimal numofDays = (decimal)(data.BookingReturnDate - data.BookingRecievedDate).TotalDays;
			        price = numofDays * car.PricePerDay;
		        }
	        }

	        return price;
        }
	}
}
