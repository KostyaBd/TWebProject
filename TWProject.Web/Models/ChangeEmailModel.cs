using System.ComponentModel.DataAnnotations;

namespace TWProject.Web.Models
{
     public class ChangeEmailModel
     {
          [Required]
          [EmailAddress]
          public string CurrentEmail { get; set; }

          [Required]
          [EmailAddress]
          public string NewEmail { get; set; }

          [Required]
          [DataType(DataType.Password)]
          public string Password { get; set; }
     }
}
