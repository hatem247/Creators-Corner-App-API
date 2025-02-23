using System.Text.Json.Serialization;

namespace Creators_Corner_App_API.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
        public List<int> Products { get; set; } = new List<int>();
        [JsonIgnore]
        public List<Product> _Products { get; set; } = new List<Product>();
    }
}
