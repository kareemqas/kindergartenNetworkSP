using System.ComponentModel.DataAnnotations;

namespace DTO.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public int Remember { get; set; }
        public string ReturnUrl { get; set; }
        public string Language { get; set; }
    }
}