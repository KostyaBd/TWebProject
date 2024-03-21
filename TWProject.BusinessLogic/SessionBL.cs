using TWProject.BusinessLogic.Core;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWProject.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResp UserLogin(ULoginData data)
        {
            throw new NotImplementedException();
        }

       
    }
}
