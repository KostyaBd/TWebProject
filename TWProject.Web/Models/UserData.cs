using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TWProject.Web.Models
{
    public class UserData
    {
        public string Username { get; set; }
        public List<string> Products { get; set; }
        public string Description { get; set; }

    }
}