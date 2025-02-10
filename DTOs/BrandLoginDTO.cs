using System.ComponentModel.DataAnnotations;

namespace Creators_Corner_App_API.DTOs
{
    public class BrandLoginDTO
    {
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }
    }
}
