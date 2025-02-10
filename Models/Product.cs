namespace Creators_Corner_App_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Foreign key to Brand (ID-based)
        public int BrandId { get; set; }

        // Navigation property for Brand
        public Brand Brand { get; set; }

        // Navigation property for Orders
        public List<Order> Orders { get; set; } = new List<Order>();

        // Navigation property for Carts
        public List<Cart> Carts { get; set; } = new List<Cart>();
    }
}
