using System;
using TWProject.Domain.Enums;

namespace TWProject.Domain.Entities.User
{
    public class UserMini
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LastIp { get; set; }
        public URoles Level { get; set; }
    }
}
