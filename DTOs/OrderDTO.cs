namespace Creators_Corner_App_API.DTOs
{
    public class OrderDTO
    {
        public int customerId { get; set; }
        public List<int> productIds { get; set; }
    }
}
