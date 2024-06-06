using System.Web;
using TWProject.Domain.Entities.User;
using TWProject.BusinessLogic.Core;
using TWProject.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;

namespace TWProject.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResp UserLogin(ULoginData uLoginData)
        {
            return UserLoginLogic(uLoginData);
        }

        public URegisterResp UserRegistration(URegisterData uRegisterData)
        {
            ULoginResp loginResp = UserRegistrationLogic(uRegisterData);
            URegisterResp registerResp = new URegisterResp { Status = loginResp.Status, StatusMsg = loginResp.StatusMsg };
            return registerResp;
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public UserMini GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        public IEnumerable<CarDBTable> GetAllCars()
        {
	        return GetAllCarsAction();
        }

        public IEnumerable<BookingDBTable> GetBookingDates()
        {
	        return GetBookingDatesAction();
        }

        public Task<UBookingResp> UserBooking(UBookingData data)
        {
	        return UserBookingAction(data);
        }


	}
}