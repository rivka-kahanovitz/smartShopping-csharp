using System.ComponentModel.DataAnnotations;

namespace Service.DTOs
{
    // אובייקט להעברת נתוני הרשמה
    public class UserSignUpDto
    {
        [Required(ErrorMessage = "enter name")]
        [MaxLength(255)] 
        public string Name { get; set; }

        [Required(ErrorMessage = "enter email")]
        [EmailAddress(ErrorMessage = "email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "enter password")]
        public string Password { get; set; }
    }
}
