using System.ComponentModel.DataAnnotations;

namespace MiBankWebsiteA3.Models
{
    public class AdminLogin
    {
        [Required(ErrorMessage = "Login ID is required")]

        public string LoginID { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string PasswordHash { get; set; }
    }
}
