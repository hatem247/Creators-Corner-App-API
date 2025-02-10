namespace Creators_Corner_App_API.Models
{
    public class Cart
    {
        public int Id { get; set; } // Primary key

        // Foreign key to Customer (one-to-one relationship)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Navigation property for Products (many-to-many relationship)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
