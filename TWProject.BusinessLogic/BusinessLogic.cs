using TWProject.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TWProject.BusinessLogic
{
    public class BusinessLogic
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BusinessLogic(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public TWProject.BusinessLogic.Interfaces.ISession GetSessionBL()
        {
            return new SessionBL(_httpContextAccessor);
        }
    }
}
