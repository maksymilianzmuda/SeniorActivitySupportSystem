using System.ComponentModel.DataAnnotations;

namespace SeniorActivitySupportSystem.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name ="Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
