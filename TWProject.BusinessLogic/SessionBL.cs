using System.Web;
using TWProject.Domain.Entities.User;
using TWProject.BusinessLogic.Core;
using TWProject.BusinessLogic.Interfaces;
using System;

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
    }
}