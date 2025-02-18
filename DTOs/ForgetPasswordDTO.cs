using System.ComponentModel.DataAnnotations;

namespace Creators_Corner_App_API.DTOs
{
    public class ForgetPasswordDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}/\|.+-])[A-Za-z\d!@#$%^&*()_+{}/\|.+-]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string newPassword { get; set; }
    }
}