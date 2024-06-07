using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWProject.Domain.Entities.User
{
     public class UChangeEmailData
     {
          public string CurrentEmail { get; set; }
          public string NewEmail { get; set; }
          public string Password { get; set; }
     }
}
