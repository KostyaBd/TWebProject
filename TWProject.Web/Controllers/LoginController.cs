using TWProject.Web.Models;
using TWProject.BusinessLogic;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.User;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

using Microsoft.AspNetCore.Http;
using TWProject.BusinessLogic.DB;

namespace TWProject.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly TWProject.BusinessLogic.Interfaces.ISession _session;
        private readonly CarRentalContext _context = new CarRentalContext();

        public LoginController()
        {
            
        }
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            var bl = new TWProject.BusinessLogic.BusinessLogic(httpContextAccessor);
            _session = bl.GetSessionBL();
        }
        public ActionResult Index()
        {
            return View();
        }
        // GET: Login
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserLogin, ULoginData>());
                var mapper = config.CreateMapper();
                var data = mapper.Map<ULoginData>(login);

                var userLogin = _session.UserLogin(data);

                if (userLogin == null)
                {
                    throw new AuthenticationException("ERROR. No login response!");
                }

                if (userLogin.Status)
                {
                    // Generating cookie for the current session
                    var apiCookie = _session.GenCookie(login.Credential);
                    var cookie = new HttpCookie("X-KEY", apiCookie.Value);

                    if (apiCookie.Options.Expires.HasValue)
                    {
                        cookie.Expires = apiCookie.Options.Expires.Value.DateTime;
                    }

                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Autentificare nereusita
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View("Login", login); ;
                }
            }


            return RedirectToAction("Index", "Home");
        }
        public UserMini GetUserDetails(string authToken)
        {
            return _session.GetUserByCookie(authToken);
        }
    }
}