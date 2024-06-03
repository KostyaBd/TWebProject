using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWProject.Domain.Enums;

namespace TWProject.Domain.Entities.User
{
	public class URegisterData
	{
        public string Username { get; set; }
        public string Email { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Password { get; set; }

        public DateTime RegisterDateTime { get; set; }
        public URoles Level { get; set; }
    }
}
