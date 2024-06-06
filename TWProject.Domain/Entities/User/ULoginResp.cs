using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Enums;

namespace TWProject.Domain.Entities.User
{
    public class ULoginResp
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public URoles Role {  get; set; }
        public bool IsAdmin { get; set; }
    }
}
