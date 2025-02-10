using System.ComponentModel.DataAnnotations;

namespace Creators_Corner_App_API.DTOs
{
    public class BrandApplicationDTO
    {
        [Required(ErrorMessage = "Brand name is required")]
        public string brandName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string instagramAccount { get; set; }
    }
}
