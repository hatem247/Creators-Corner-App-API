namespace Creators_Corner_App_API.DTOs
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerUsername { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
