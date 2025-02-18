namespace Creators_Corner_App_API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public List<int> Orders { get; set; } = new List<int>();
        public List<Order> _Orders { get; set; } = new List<Order>();
    }
}
