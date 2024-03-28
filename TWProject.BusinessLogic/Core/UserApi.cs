using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.BusinessLogic.DB;
using TWProject.Domain.Entities.User;

namespace TWProject.BusinessLogic.Core
{
    public class UserApi
    {
	    internal ULoginResp UserLoginLogic(ULoginData data)
	    {
		    using (var context = new CarRentalContext())
		    {
			    var result = context.User.FirstOrDefault(u => u.Name == data.Credential && u.Password == data.Password);
			    if (result == null)
			    {
				    return new ULoginResp { Status = false, StatusMsg = "Wrong email or password" };
			    }
		    }

		    return new ULoginResp { Status = true };
	    }

	    internal URegisterResp UserRegistrationLogic(URegisterData data)
	    {
		    using (var context = new CarRentalContext())
		    {
			    var result = context.User.FirstOrDefault(u => u.Name == data.Username);
			    if (result != null)
			    {
				    return new URegisterResp { Status = false, StatusMsg = "User with this name already exists" };
			    }

			    var newUser = new UDBTable
			    {
					Name = data.Username,
					Password = data.Password,
					Email = data.Email
			    };
				context.User.Add(newUser);
				context.SaveChanges();
				return new URegisterResp { Status = true };
		    }
	    }
	}
}
