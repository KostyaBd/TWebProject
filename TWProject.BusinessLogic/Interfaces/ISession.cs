using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.User;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace TWProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
		URegisterResp UserRegistration(URegisterData data);

        (string Value, CookieOptions Options) GenCookie(string loginCredential);
        UserMini GetUserByCookie(string apiCookieValue);

    }
}
