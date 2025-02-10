using Creators_Corner_App_API.DTOs;
using Creators_Corner_App_API.Models;

namespace Creators_Corner_App_API.Repositories.Customer_Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> LoginAsync(string username, string password);
        Task RegisterAsync(CustomerDTO customerDto);
        Task BuyProductAsync(int productId, string customerUsername);
        Task<List<Product>> GetAllProductsAsync();
        Task<string> CompareProductsAsync(int productId1, int productId2);
        Task ForgetPasswordAsync(string email);
        Task<Cart> GetCustomerCartAsync(string customerUsername);
        Task AddProductToCartAsync(int productId, string customerUsername);
    }
}
