using System.Text.Json.Serialization;

namespace Creators_Corner_App_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<byte[]> Images { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int BrandId { get; set; }
        [JsonIgnore]
        public Brand Brand { get; set; }
        public List<int> Orders { get; set; } = new List<int>();
        [JsonIgnore]
        public List<Order> _Orders { get; set; } = new List<Order>();
        public List<int> Carts { get; set; } = new List<int>();
        [JsonIgnore]
        public List<Cart> _Carts { get; set; } = new List<Cart>();
    }
}
