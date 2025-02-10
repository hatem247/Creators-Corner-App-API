namespace Creators_Corner_App_API.Models
{
    public class Order
    {
        public int Id { get; set; } // Primary key

        public DateTime OrderDate { get; set; } // Date of the order
        public decimal TotalAmount { get; set; } // Total amount of the order

        // Foreign key to Customer (many-to-one relationship)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Navigation property for Products (many-to-many relationship)
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
