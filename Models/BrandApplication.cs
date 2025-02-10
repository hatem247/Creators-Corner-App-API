namespace Creators_Corner_App_API.Models
{
    public class BrandApplication
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string Email { get; set; }
        public string InstagramAccount { get; set; }
        public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
    }
}
