using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.User;
using TWProject.Web.Models;

namespace TWProject.Web.Controllers
{
    public class RegisterController : Controller
    {
	    private readonly ISession _session;
	    public RegisterController()
	    {
		    var bl = new BusinessLogic.BusinessLogic();
		    _session = bl.GetSessionBL();
	    }
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserRegister register)
        {
	        if (ModelState.IsValid)
	        {
		        URegisterData data = new URegisterData()
		        {
					Username = register.Username,
					Password = register.Password,
					Email = register.Email
		        };
		        var userRegister = _session.UserRegistration(data);
		        return View();
	        }
        }
    }
}