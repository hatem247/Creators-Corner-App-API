using System.Text.Json.Serialization;

namespace Creators_Corner_App_API.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool FirstLogin { get; set; } = true;
        public List<int> Products { get; set; } = new List<int>();
        [JsonIgnore]
        public List<Product> _Products { get; set; } = new List<Product>();
    }
}
