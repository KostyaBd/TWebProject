using System;
using System.Web;
using System.Web.Mvc;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Web.Models;
using TWProject.Domain.Entities.User;
using System.Threading.Tasks;
using System.Collections.Generic;
using TWProject.Domain.Entities.Booking;

namespace TWProject.Web.Controllers
{
     public class UserController : Controller
     {
          private readonly ISession _session;

          public UserController()
          {
               var bl = new BusinessLogic.BusinessLogic();
               _session = bl.GetSessionBL();
          }

          public ActionResult Index()
          {
               if (!IsUserLoggedIn())
               {
                    return RedirectToAction("Index", "Login");
               }
               return View();
          }

          public ActionResult ChangePassword()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult ChangePassword(ChangePasswordModel model)
          {
               if (ModelState.IsValid)
               {
                    var changePasswordData = new UChangePasswordData
                    {
                         Email = model.Email,
                         CurrentPassword = model.CurrentPassword,
                         NewPassword = model.NewPassword,
                         ConfirmPassword = model.ConfirmPassword
                    };

                    var result = _session.ChangePassword(changePasswordData);

                    if (result.Success)
                    {
                         ViewBag.SuccessMessage = "Password changed successfully.";
                         return View();
                    }
                    else
                    {
                         ModelState.AddModelError("", result.ErrorMessage);
                         return View(model);
                    }
               }

               return View(model);
          }

          public ActionResult ChangeEmail()
          {
               return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public ActionResult ChangeEmail(ChangeEmailModel model)
          {
               if (ModelState.IsValid)
               {
                    var changeEmailData = new UChangeEmailData
                    {
                         CurrentEmail = model.CurrentEmail,
                         NewEmail = model.NewEmail,
                         Password = model.Password
                    };

                    var result = _session.ChangeEmail(changeEmailData);

                    if (result.Success)
                    {
                         ViewBag.SuccessMessage = "Email changed successfully.";
                         return View();
                    }
                    else
                    {
                         ModelState.AddModelError("", result.ErrorMessage);
                         return View(model);
                    }
               }

               return View(model);
          }
        public ActionResult UserBookings()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Login");
            }

            var user = GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var bookings = _session.GetBookingsByUser(user.Id);

            return View(bookings);
        }

        private UserMini GetCurrentUser()
        {
            var cookieValue = HttpContext.Request.Cookies["X-KEY"]?.Value;
            return _session.GetUserByCookie(cookieValue);
        }

        private bool IsUserLoggedIn()
          {
               var loginStatus = System.Web.HttpContext.Current.Session["LoginStatus"] as string;
               return loginStatus == "login";
          }
     }
}
