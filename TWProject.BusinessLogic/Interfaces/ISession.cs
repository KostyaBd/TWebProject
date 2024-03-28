using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Entities.User;

namespace TWProject.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
		URegisterResp UserRegistration(URegisterData data);
	}
}
