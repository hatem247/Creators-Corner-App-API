using System.ComponentModel.DataAnnotations;

namespace Creators_Corner_App_API.DTOs
{
    public class BrandDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string name { get; set; }
        
        [StringLength(300, ErrorMessage = "Bio cannot exceed 300 characters")]
        public string bio { get; set; }

        [Required(ErrorMessage = "No image uploaded")]
        public string image { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}/\|.+-])[A-Za-z\d!@#$%^&*()_+{}/\|.+-]{8,16}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string password { get; set; }
    }
}
