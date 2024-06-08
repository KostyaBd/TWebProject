using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.User;
using System.Web;
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;

namespace TWProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        URegisterResp UserRegistration(URegisterData uRegisterData);

        HttpCookie GenCookie(string loginCredential);

        UserMini GetUserByCookie(string apiCookieValue);

        IEnumerable<CarDBTable> GetAllCars();

        IEnumerable<BookingDBTable> GetBookingDates();

        Task<UBookingResp> UserBooking(UBookingData data);

        UChangePasswordResp ChangePassword(UChangePasswordData data);
        UChangeEmailResp ChangeEmail(UChangeEmailData data);
     }
}