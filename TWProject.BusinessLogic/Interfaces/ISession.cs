using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.User;
using System.Web;

namespace TWProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
        URegisterResp UserRegistration(URegisterData uRegisterData);

        HttpCookie GenCookie(string loginCredential);

        UserMini GetUserByCookie(string apiCookieValue);

    }
}