using System.ComponentModel.DataAnnotations;

namespace Creators_Corner_App_API.DTOs
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "No image uploaded")]
        public string image { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string address { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*()_+{}/\|.+-])[A-Za-z\d!@#$%^&*()_+{}/\|.+-]{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain at least one letter, one number, and one special character.")]
        public string password { get; set; }
    }
}
