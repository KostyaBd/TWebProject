using TWProject.Web.Models;
using TWProject.BusinessLogic;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.User;
using TWProject.Domain.Enums; // Ensure this is included
using System;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace TWProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISession _session;

        public LoginController()
        {
            var bl = new TWProject.BusinessLogic.BusinessLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserLogin, ULoginData>());
                var mapper = config.CreateMapper();

                ULoginData data = new ULoginData
                {
                    Credential = login.Credential,
                    Password = login.Password,
                };

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    HttpCookie cookie = _session.GenCookie(login.Credential);
                    cookie.HttpOnly = true; 
                    cookie.Expires = DateTime.Now.AddHours(1); 
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                   
                    if (userLogin.Role == URoles.Admin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View("Login"); 
                }
            }

            return View("Index");
        }

        public UserMini GetUserDetails(string authToken)
        {
            return _session.GetUserByCookie(authToken);
        }

        public ActionResult Logout()
        {
            if (Request.Cookies["X-KEY"] != null)
            {
                var cookie = new HttpCookie("X-KEY");
                cookie.Expires = DateTime.Now.AddDays(-1); 
                Response.Cookies.Add(cookie); 
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
