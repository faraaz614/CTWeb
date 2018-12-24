using System.ComponentModel.DataAnnotations;

namespace CT.Web.Models
{
    public class LoginModel : BaseModel
    {
        [Required(ErrorMessage = "*Required")]
        [Display(Name = "User name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "*Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}