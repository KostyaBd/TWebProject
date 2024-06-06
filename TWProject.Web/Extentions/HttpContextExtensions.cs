using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TWProject.Domain.Entities.User;

namespace TWProject.Web.Extentions
{
	public static  class HttpContextExtensions
	{
		public static UserMini GetMySessionObject(this HttpContext current)
		{
			return (UserMini)current?.Session["__SessionObject"];
		}

		public static void SetMySessionObject(this HttpContext current, UserMini profile)
		{
			current.Session.Add("__SessionObject", profile);
		}

		
	}
}