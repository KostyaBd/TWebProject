using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;

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
        IEnumerable<BookingDBTable> GetBookingsByUser(int userId);

    }
}
