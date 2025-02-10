namespace Creators_Corner_App_API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Username { get; set; } // Unique username
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
