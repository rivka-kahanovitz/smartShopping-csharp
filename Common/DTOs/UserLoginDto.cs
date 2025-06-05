using System.ComponentModel.DataAnnotations;

namespace common.DTOs
{
    // אובייקט להעברת נתוני התחברות
    public class UserLoginDto
    {
        [Required(ErrorMessage = "enter email")]
        [EmailAddress(ErrorMessage = "email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "enter password")]
        public string Password { get; set; }
    }
}
