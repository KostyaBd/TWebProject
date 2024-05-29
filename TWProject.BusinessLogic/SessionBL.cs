using Microsoft.AspNetCore.Http;
using TWProject.Domain.Entities.User;
using TWProject.BusinessLogic.Core;
using TWProject.BusinessLogic.Interfaces;

namespace TWProject.BusinessLogic
{
    public class SessionBL : UserApi, TWProject.BusinessLogic.Interfaces.ISession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionBL(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginLogic(data);
        }

        public URegisterResp UserRegistration(URegisterData data)
        {
            return UserRegistrationLogic(data);
        }

        public (string Value, CookieOptions Options) GenCookie(string loginCredential)
        {
            return CreateCookie(loginCredential);
        }

        public UserMini GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
